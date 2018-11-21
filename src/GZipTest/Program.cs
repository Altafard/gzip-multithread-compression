using GZipTest.Abstractions;
using GZipTest.Factories;
using GZipTest.Handlers;
using System;
using System.Diagnostics;
using System.Text;

namespace GZipTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args == null || args.Length != 3)
            {
                Help();
                return;
            }

            try
            {
                CommandType commandType;
                if (TryParseCommandType(args[0], out commandType) == false)
                {
                    Help();
                    return;
                }

                using (IGZipHandler handler = CreateHandler(commandType, args[1], args[2]))
                {
                    Console.WriteLine("Starting {0} a file {1}", args[0], args[1]);

                    var sw = new Stopwatch();
                    sw.Start();

                    handler.Handle();

                    sw.Stop();

                    Console.WriteLine("Completed in {0}ms. Result is {1}", sw.ElapsedMilliseconds, args[2]);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Unhandled exception occurred: {0}: {1}", exception.GetType().Name, exception.Message);
            }
        }

        private static bool TryParseCommandType(string command, out CommandType type)
        {
            type = CommandType.Unknown;
            try
            {
                type = (CommandType)Enum.Parse(typeof(CommandType), command, true);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch
            {
                throw;
            }
        }

        private static IGZipHandler CreateHandler(CommandType command, string src, string dest)
        {
            switch (command)
            {
                case CommandType.Compress:
                    var compressionFactory = new CompressionFactory(src, dest);
                    return new CompressionHandler(compressionFactory);
                case CommandType.Decompress:
                    var decompressionFactory = new DecompressionFactory(src, dest);
                    return new DecompressionHandler(decompressionFactory);
                default: return null;
            }
        }

        private static void Help()
        {
            string help = new StringBuilder()
                .AppendLine("USAGE: GZipTest [command] [source] [destination]")
                .AppendLine("where [command] is:")
                .AppendLine("\tcompress\t: compress source file and save it in destination path")
                .AppendLine("\tdecompress\t: decompress source file and save it in destination path")
                .AppendLine("[source] and [destination]: paths to files")
                .ToString();
            Console.WriteLine(help);
        }
    }
}
