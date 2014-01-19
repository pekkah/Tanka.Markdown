using System.Diagnostics;

Build();
Test();


public void Build() {
	MSBuild.BuildSolution(@"src\Tanka.Markdown.sln", Path.GetFullPath("build"));
}

public void Test() {
	var files = Directory.GetFiles(@"build\", "*Tests.dll");

	foreach (var file in files) {
		RunTests(file);
	}
}

public void RunTests(string assemblyPath) {
	var runner = Require<XunitRunner>();

	Console.WriteLine("Executing tests from {0}", assemblyPath);
	var result = runner.Execute(assemblyPath);

	Console.WriteLine("Executed  {0} tests - failed - {1} - total time {0} seconds", 
		result.TestsCount,
		result.TestsFailed,
		result.TotalTimeInSeconds);
}

public static class MSBuild {
	public static void BuildSolution(string solutionFilePath, string outputPath) {

		 var info = new ProcessStartInfo("msbuild.exe");
		 info.Arguments = string.Format("{0} /p:OutputPath={1}", solutionFilePath, outputPath);

		 var process = Process.Start(info);
	}
}