using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.CommandLineUtils;

namespace FileGenerator
{
    public class Program
    {
        private const long DefaultFileSize = 5242880; // Bytes (5MB)

        public static void Main(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "dotnet fg.dll",
                Description = "Generates the text file of given size",
                ExtendedHelpText = "\nGenerates the text file of given size",
                FullName = "File Generator"
            };

            app.HelpOption("-? | -h | --help");
            app.VersionOption("-v | --version", () => $"Version {Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion}");

            app.Command("generate", command =>
            {
                command.FullName = "Command of the file generation";
                command.Description = "Generates the file";
                command.ExtendedHelpText = "\nGenerates the file";
                command.HelpOption("-? | -h | --help");

                CommandArgument path = command.Argument("path", "Path with file name to generate");
                CommandOption option = command.Option("-s | --size <value>", "File size in bytes (default is 5MB)", CommandOptionType.SingleValue);

                command.OnExecute(async () =>
                {
                    if (string.IsNullOrEmpty(path.Value))
                        throw new ArgumentNullException(nameof(path));

                    if (option.HasValue() == false || long.TryParse(option.Value(), out long size) == false)
                    {
                        size = DefaultFileSize;
                    }

                    await GenerateFile(path.Value, size);

                    return 0;
                });
            });

            try
            {
                app.Execute(args);
            }
            catch (CommandParsingException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(@"Unable to execute application: {0}", e.Message);
            }
        }

        private static async Task GenerateFile(string path, long size)
        {
            Console.WriteLine(@"Generating the file {0} with size of {1} bytes", path, size);

            byte[] bytes = Encoding.UTF8.GetBytes(Resource.Text);
            using (FileStream fs = File.Open(path, FileMode.Create, FileAccess.Write))
            {
                Console.Write(@"Recorded ");
                int left = Console.CursorLeft;
                int top = Console.CursorTop;

                long totalProgress = 0;
                while (fs.Length != size)
                {
                    int count = size - fs.Length < bytes.Length
                        ? (int) (size - fs.Length)
                        : bytes.Length;

                    await fs.WriteAsync(bytes, 0, count);
                    
                    long progress = 100 * fs.Length / size;
                    if (progress <= totalProgress) continue;

                    Console.SetCursorPosition(left, top);
                    Console.Write(@"{0}%", progress);
                    totalProgress = progress;
                }

                Console.WriteLine();
            }
        }
    }
}
