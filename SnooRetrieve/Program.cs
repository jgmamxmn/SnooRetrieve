using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SnooRetrieve
{
	internal class Program
	{
		static void Main(string[] args) 
		{
			//DumpAllHistory("****@gmail.com", "*****", new DateOnly(2022, 11, 25), new DateOnly(2023, 04, 20), "E:\\Docs\\OneDrive\\Aadi\\SnooHistory_.json");
			RenderAll("E:\\Docs\\OneDrive\\Aadi\\SnooHistory_.json", "E:\\Docs\\OneDrive\\Aadi\\SnooHistory.html");
		}


	}
}