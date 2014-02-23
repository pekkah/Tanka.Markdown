using System.Diagnostics;

/* configuration */
var config = new {
	buildOut = "build",
	solutionFilePath = @"src\Tanka.Markdown.sln",
	testsPattern = @"*Tests.dll"
};


/* build steps */
Clean();

var buildResult = Build();

if (buildResult) {
	Console.WriteLine("Build passed");

	if (Test())	{
		Console.WriteLine("Tests passed.");
	}
	else {
	    Console.WriteLine("Tests failed!");
	}
}
else {
    Console.WriteLine("Build failed!");
}


/* build tasks */
public void Clean() {
	Console.WriteLine("Clean directory {0}", config.buildOut);

	var fullPath = Path.GetFullPath(config.buildOut);

	if (Directory.Exists(fullPath))
		Directory.Delete(fullPath, recursive: true);
}

public bool Build() {
	Console.WriteLine("Build solution {0}", config.solutionFilePath);
	var exitCode = BuildSolution(config.solutionFilePath, config.buildOut);

	return exitCode == 0 ? true: false;
}

public bool Test() {
	Console.WriteLine("Execute tests");

	var files = Directory.GetFiles(config.buildOut, config.testsPattern);

	bool testsPassed = true;

	foreach (var file in files) {
		if (!RunTests(file))
			return false;
	}

	return testsPassed;
}

public bool RunTests(string assemblyPath) {
	var runner = Require<XunitRunner>();

	Console.WriteLine("Executing tests from {0}", assemblyPath);
	var result = runner.Execute(assemblyPath);

	Console.WriteLine("Executed  {0} tests - failed - {1} - total time {0} seconds", 
		result.TestsCount,
		result.TestsFailed,
		result.TotalTimeInSeconds);

	return result.TestsFailed > 0 ? false: true;
}

public static int BuildSolution(string solutionFilePath, string outputPath) {

	 var info = new ProcessStartInfo("msbuild.exe");
	 info.Arguments = string.Format("{0} /p:OutputPath={1} /t:rebuild", solutionFilePath, Path.GetFullPath(outputPath));

	 var process = Process.Start(info);
	 process.WaitForExit();

	 return process.ExitCode;
}