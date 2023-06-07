using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnooRetrieve
{
	public static class KeyFunctions
	{
		private static DateOnly DTtoDate(DateTime _adate)
		{
			return new DateOnly(_adate.Year, _adate.Month, _adate.Day);
		}

		public static void RenderAll(string inputJsonFilePath, string outputHtmlFilePath)
		{
			Dictionary<DateOnly, DayResult> Days = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<DateOnly, DayResult>>(File.ReadAllText(inputJsonFilePath));
			RenderAll(Days, outputHtmlFilePath);
		}

		public static void RenderAll(Dictionary<DateOnly, DayResult> Days, string outputHtmlFilePath)
		{
			DateOnly FirstDay = DTtoDate(Days.First().Value.levels.First().startTimeFormatted);
			DateOnly LastDay = FirstDay;

			var Sessions = new List<Level>();
			foreach (var Day in Days)
			{
				if (Day.Key < FirstDay)
					FirstDay = Day.Key;
				if (Day.Key > LastDay)
					LastDay = Day.Key;

				foreach (var L in Day.Value.levels)
				{
					var EndTime = L.startTimeFormatted.AddSeconds(L.stateDuration);

					if (L.startTimeFormatted.Day != EndTime.Day)
					{
						var EndDay1 = new DateTime(L.startTimeFormatted.Year, L.startTimeFormatted.Month, L.startTimeFormatted.Day, 23, 59, 59);
						var StartDay2 = new DateTime(EndTime.Year, EndTime.Month, EndTime.Day, 0, 0, 0);

						var Day1Duration = (int)((EndDay1 - L.startTimeFormatted).TotalSeconds) + 1;

						// TODO split into separate days
						Sessions.Add(new Level
						{
							sessionId = L.sessionId,
							isActive = L.isActive,
							startTime = L.startTime,
							startTimeFormatted = L.startTimeFormatted,
							type = L.type,
							stateDuration = Day1Duration
						});

						Sessions.Add(new Level
						{
							sessionId = L.sessionId,
							isActive = L.isActive,
							startTime = L.startTime,
							startTimeFormatted = StartDay2,
							type = L.type,
							stateDuration = L.stateDuration - Day1Duration
						});

					}
					else
					{
						Sessions.Add(L);
					}
				}
			}

			var HTMLHeaders = new List<string>();
			var HTML = new List<string>();
			int RowHeight = 16;
			int BarHeight = 8;
			int MarginLeft = 64;
			int HourWidth = 64;
			var font = "font-family:Arial;font-size:8pt;";

			for (var aDate = FirstDay; aDate <= LastDay; aDate = aDate.AddDays(1))
			{
				HTMLHeaders.Add($"<div style=\"position:absolute;left:1px;width:{MarginLeft}px;{font}top:{(Subtract(aDate, FirstDay).TotalDays + 1) * RowHeight}px;\">" + aDate.ToString("dd/MM/yyyy") + "</div>");
			}

			for (int Hr = 0; Hr < 24; ++Hr)
			{
				int refHr = Hr;
				if (refHr > 12)
					refHr = 12 - refHr;
				byte bColor = (byte)((200.0 * refHr) / 12.0);
				string sColor = bColor.ToString("X2");


				HTMLHeaders.Add($"<div style=\"position:absolute;left:{MarginLeft + Hr * HourWidth}px;top:1px;{font}border-left:1px solid #{sColor}{sColor}{sColor};color:#{sColor}{sColor}{sColor};\">" + Hr.ToString("00") + ":00</div>");
			}

			foreach (var Ses in Sessions)
			{
				int DayNum = (int)Subtract(DTtoDate(Ses.startTimeFormatted), FirstDay).TotalDays;
				double StartHr = ((double)Ses.startTimeFormatted.Hour) + (((double)Ses.startTimeFormatted.Minute) / 60.0) + (((double)Ses.startTimeFormatted.Second) / 3600.0);
				DateTime endTime = Ses.startTimeFormatted.AddSeconds(Ses.stateDuration);
				double EndHr = ((double)endTime.Hour) + (((double)endTime.Minute) / 60.0) + (((double)endTime.Second) / 3600.0);

				int width = (int)(HourWidth * (EndHr - StartHr));
				if (width == 0) // skip zero-width records
					continue;

				string color = Ses.type switch
				{
					"asleep" => "blue",
					"soothing" => "red",
					_ => "lightgray"
				};

				int left = (int)(MarginLeft + (HourWidth * StartHr));
				int top = (DayNum + 1) * RowHeight
					+ ((RowHeight - BarHeight) / 2);

				HTML.Add($"<div style=\"position:absolute;top:{top}px;height:{BarHeight}px;left:{left}px;width:{width}px;background:{color};\">&nbsp;</div>");
			}

			File.WriteAllText(outputHtmlFilePath, new System.Text.StringBuilder().Append("<html><head></head><body>").Append(string.Concat(HTMLHeaders)).Append(string.Concat(HTML)).Append("</body></html>").ToString());
		}

		public static TimeSpan Subtract(DateOnly later, DateOnly earlier)
		{
			var dtLater = new DateTime(later.Year, later.Month, later.Day);
			var dtEarlier = new DateTime(earlier.Year, earlier.Month, earlier.Day);
			return dtLater - dtEarlier;
		}


		public static async Task DumpAllHistory_JSON(string username, string password, DateOnly startDate, DateOnly lastDate, string outputJsonFile)
		{
			var days = await RetrieveAllHistory(username, password, startDate, lastDate);
			DumpAllHistory_JSON(days, outputJsonFile);
		}
		public static void DumpAllHistory_JSON(Dictionary<DateOnly, DayResult> days, string outputJsonFile)
		{
			File.WriteAllText(outputJsonFile, Newtonsoft.Json.JsonConvert.SerializeObject(days, Newtonsoft.Json.Formatting.Indented));
		}
		public static void DumpAllHistory_CSV(Dictionary<DateOnly, DayResult> days, string outputCsvFile)
		{
			var rows = new List<string>();

			var Props = typeof(Level).GetProperties();

			rows.Add(string.Join(",", Props.Select(p => p.Name)));
			foreach (var day in days)
				foreach (var l in day.Value.levels)
					rows.Add(string.Join(",", Props.Select(p => p.GetValue(l).ToString())));
			
			File.WriteAllText(outputCsvFile, string.Join("\r\n", rows));
		}

		public static async Task<Dictionary<DateOnly, DayResult>> RetrieveAllHistory(string username, string password, DateTime startDate, DateTime lastDate, Action<DateOnly> cbkProgress = null)
			=> await RetrieveAllHistory(username, password, DTtoDate(startDate), DTtoDate(lastDate), cbkProgress);

		public static async Task<Dictionary<DateOnly, DayResult>> RetrieveAllHistory(string username, string password, DateOnly startDate, DateOnly lastDate, Action<DateOnly> cbkProgress=null)
		{
			var token = await ApiQuery.GetToken(username, password);

			var days = new Dictionary<DateOnly, DayResult>();

			for (var d = startDate; d <= lastDate; d = d.AddDays(1))
			{
				//Console.WriteLine(d.ToString("yyyy-MM-dd"));
				if (cbkProgress != null)
					cbkProgress(d);

				var day = await ApiQuery.GetDayHistory(token, d);
				days.Add(d, day);
			}

			return days;
		}
	}
}
