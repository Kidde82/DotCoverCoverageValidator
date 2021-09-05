using System;
using System.Collections.Generic;
using System.IO;
using DotCoverCoverageValidator.Model;
using Newtonsoft.Json;

namespace DotCoverCoverageValidator
{
    class Program
    {
        static int Main(string[] args)
        {
            int threshold = 0;
            var dotCoverOutputPath = "dotCover.Output.json";

            if (args.Length == 0) {
                WriteError(ErrorType.ThresholdMissing);
                return 1;
            }

            for (int i = 0; i < args.Length; i++) {
                var arg = args[i].ToLower();

                if (arg == "--threshold") {
                    if (ArgumentHasValue(args, i)) {
                        int thresholdNumber;
                        var success = int.TryParse(args[i + 1], out thresholdNumber);
                        if (success == false) {
                            WriteError(ErrorType.ThresholdType);
                            return 1;
                        }
                        threshold = thresholdNumber;                        
                    } else {
                        WriteError(ErrorType.ThresholdType);
                        return 1;
                    }
                }

                if (arg == "--dotcoveroutputpath") {
                    if (ArgumentHasValue(args, i)) {
                        dotCoverOutputPath = args[i + 1];
                    } else {
                        WriteError(ErrorType.OutpoutPathMissing);
                        return 1;
                    }
                }
            }

            DotCoverOutput dotCoverOutput = null;

            try {
                dotCoverOutput = LoadJson(dotCoverOutputPath);
            } catch (Exception ex){
                Console.WriteLine($"Could not load or parse json file: {dotCoverOutputPath}\n{ex.Message}");
                return 1;
            }

            if (dotCoverOutput.CoveragePercent > threshold) {
                Console.WriteLine($"Threshold passed!\nCoverage: {dotCoverOutput.CoveragePercent}\nThreshold: {threshold}.");
                return 0;
            } else {
                Console.WriteLine($"Threshold did not pass!\nCoverage: {dotCoverOutput.CoveragePercent}\nThreshold: {threshold}.");
                return 1;
            }
        }

        static DotCoverOutput LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<DotCoverOutput>(json);

                return items;
            }
        }

        static bool ArgumentHasValue(string[] args, int i) {
            return args.Length > (i+1);
        }

        static void WriteError(ErrorType errorType) {
            switch (errorType) {
                case ErrorType.ThresholdMissing: {
                    Console.WriteLine("--threshold is a required flag");
                    break;
                }
                case ErrorType.ThresholdType: {
                    Console.WriteLine("--threshold is a required flag which needs to be a number");
                    break;
                }
                case ErrorType.OutpoutPathMissing: {
                    Console.WriteLine("--dotcoveroutputpath needs to be a string");
                    break;
                }
            }
        }
    }
}
