// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Setup.Configuration;

internal class Program
{
    private static uint REGDB_E_CLASSNOTREG = 2147746132u;

    private static void Main(string[] args)
    {
        string visualStudioVersion = "2022";
        if (args != null && args.Length == 1)
        {
            visualStudioVersion = args[0];
        }
        string microsoftVisualStudio = "Microsoft Visual Studio";

        List<string> allInstances = FindVisualStudioSetupInstancePath();
        IEnumerable<string> filteredInstances = allInstances.Where(x => x.Contains(microsoftVisualStudio) && x.Contains(visualStudioVersion));
        List<string> instancesList = filteredInstances.ToList();

        if (instancesList.Count > 1)
        {
            Console.WriteLine("Several instances found. Please select one:");
            for (int i = 0; i < instancesList.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {instancesList[i]}");
            }

            int selectedInstanceIndex = GetUserChoice(instancesList.Count) - 1;
            SetEnvironmentVariable("VCDIR", instancesList[selectedInstanceIndex]);
        }
        else if (instancesList.Count == 1)
        {
            SetEnvironmentVariable("VCDIR", instancesList[0]);
        }
        else
        {
            Console.WriteLine("ERROR: No suitable Visual Studio instances found.");
        }
    }

    private static int GetUserChoice(int maxOption)
    {
        int choice = 0;
        while (true)
        {
            Console.Write("Enter your choice (number): ");
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= maxOption)
            {
                return choice;
            }
            Console.WriteLine("Invalid choice, please select a valid option.");
        }
    }

    private static void SetEnvironmentVariable(string variableName, string path)
    {
        try
        {
            Environment.SetEnvironmentVariable(variableName, path, EnvironmentVariableTarget.User);
            Console.WriteLine($"Variable {variableName}={path} has been successfully set.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Variable {variableName} has not been set successfully, due to an error.");
            Console.WriteLine(ex);
        }
    }

    public static List<string> FindVisualStudioSetupInstancePath()
    {
        List<string> list = new List<string>();
        try
        {
            SetupConfiguration setupConfiguration = new SetupConfiguration();
            ISetupConfiguration2 setupConfiguration2 = (ISetupConfiguration2)setupConfiguration;
            IEnumSetupInstances enumSetupInstances = setupConfiguration2.EnumAllInstances();

            ISetupInstance[] instances = new ISetupInstance[1];
            int fetched;
            do
            {
                enumSetupInstances.Next(1, instances, out fetched);
                if (fetched > 0)
                {
                    list.Add(instances[0].GetInstallationPath());
                }
            } while (fetched > 0);

            return list;
        }
        catch (COMException ex) when (ex.HResult == REGDB_E_CLASSNOTREG)
        {
            Console.WriteLine("The query API is not registered. Assuming no instances are installed.");
            return list;
        }
    }
}