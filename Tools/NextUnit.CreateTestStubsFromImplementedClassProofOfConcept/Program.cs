using NextUnit.CreateTestStubsFromImplementedClassProofOfConcept;

Thread thread = new Thread(new ThreadStart(() =>
{
	try
	{
		string result = TestStubGenerator.GenerateTestStubsFromClipboard();
		Console.WriteLine("Generated Test Stubs:\n");
		Console.WriteLine(result);

		// Optionally, copy the result back to the clipboard
		ClipboardWrapper.CopyText(result);
		Console.WriteLine("\nThe generated test stubs have been copied back to the clipboard.");
	}
	catch (Exception ex)
	{
		Console.WriteLine("Error: " + ex.Message);
	}
	Console.ReadLine();
}));

thread.SetApartmentState(ApartmentState.STA);
thread.Start();
thread.Join();