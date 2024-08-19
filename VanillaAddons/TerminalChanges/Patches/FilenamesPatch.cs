using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VanillaAddons.TerminalChanges.Patches
{
    internal class FilenamesPatch
    {

        internal static bool nanoIsOpen;
        internal static FileCommands.File nanoFile;

        internal static List<FileCommands.File> files;

        internal static void Init(List<FileCommands.File> file)
        {
            nanoIsOpen = false;
            files = file;

            On.Terminal.ParsePlayerSentence += Terminal_ParsePlayerSentence;
            On.Terminal.LoadNewNode += Terminal_LoadNewNode;
        }

        private static bool IsEncrypted(string filename)
        {
            return files.FirstOrDefault(file => file.name == filename).encrypted;
        }

        private static TerminalNode FileEncrypted(string filename)
        {
            TerminalNode fileEncrypted = ScriptableObject.CreateInstance<TerminalNode>();
            fileEncrypted.displayText = $"File Unreachable: {filename} has been encrypted and connot be opened without a password.\n";
            fileEncrypted.clearPreviousText = true;
            return fileEncrypted;
        }

        private static bool IsReadOnly(string filename)
        {
            return files.FirstOrDefault(file => file.name == filename).isReadOnly;
        }

        private static TerminalNode FileReadOnly(string filename)
        {
            TerminalNode fileReadOnly = ScriptableObject.CreateInstance<TerminalNode>();
            fileReadOnly.displayText = $"Error: {filename} has been declared read-only and cannot be edited.\n";
            fileReadOnly.clearPreviousText = true;
            return fileReadOnly;
        }

        private static TerminalNode FileNotFound(string filename)
        {
            TerminalNode fileNotFound = ScriptableObject.CreateInstance<TerminalNode>();
            fileNotFound.displayText = $"File Not Found: {filename} could not be found on the desktop.\n";
            fileNotFound.clearPreviousText = true;
            return fileNotFound;
        }

        private static TerminalNode Terminal_ParsePlayerSentence(On.Terminal.orig_ParsePlayerSentence orig, Terminal self)
        {
            string s = self.screenText.text.Substring(self.screenText.text.Length - self.textAdded);
            string[] array = s.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);

            VanillaAddonsBase.Instance.mls.LogInfo($"VanillaAddons ParsePlayerSentencePatch");

            if (nanoIsOpen)
                return CloseNano(s);

            // If it is empty, continue
            if (array.Length == 0)
                return orig(self);

            // If it is a file related word,
            if (array[0].Equals("touch") ||
                array[0].Equals("cat") ||
                array[0].Equals("rm") ||
                array[0].Equals("nano") ||
                array[0].Equals("gpg"))
            {
                // Ensure there is a filename
                if (array.Length == 1)
                {
                    TerminalNode noFilename = ScriptableObject.CreateInstance<TerminalNode>();
                    noFilename.displayText = "Error: No filename specified.\n";
                    noFilename.clearPreviousText = true;
                    return noFilename;
                }

                // Go through all actions
                switch (array[0])
                {
                    case "touch":
                        {
                            return Touch(array[1]);
                        }
                    case "cat":
                        {
                            return Cat(array[1]);
                        }
                    case "rm":
                        {
                            return Remove(array[1]);
                        }
                    case "nano":
                        {
                            return OpenNano(array[1], self);
                        }
                    case "gpg":
                        {
                            return Encrypt(array);
                        }
                }
            }

            return orig(self);
        }

        // Create File
        private static TerminalNode Encrypt(string[] args)
        {
            if (args.Length <= 3)
            {
                return CreateNode($"Invalid Argument: {args[1]} Valid arguments are -c, --passphrase.\n");
            }

            // Check if it's encryption or decryption
            bool isEncrypting = args[1] == "-c";
            bool isDecrypting = args[1] == "--passphrase";

            if (!isEncrypting && !isDecrypting)
            {
                return CreateNode($"Invalid Argument: {args[1]} Valid arguments are -c, --passphrase.\n");
            }

            // Validate passphrase argument
            if (isEncrypting && args[2] != "--passphrase")
            {
                return CreateNode($"Invalid Argument: {args[2]} Valid argument is --passphrase.\n");
            }

            // Validate argument length
            if (args.Length < (isEncrypting ? 5 : 4))
            {
                return CreateNode("Missing Argument: Please provide the correct passphrase and filename.\n");
            }

            if (args.Length > (isEncrypting ? 5 : 4))
            {
                return CreateNode($"Unknown argument: {args[isEncrypting ? 5 : 4]} is unknown.\n");
            }

            if (files == null || !files.Any())
            {
                return CreateNode("No files available to encrypt or decrypt.\n");
            }

            var fileName = args[isEncrypting ? 4 : 3];
            var file = files.FirstOrDefault(f => f.name == fileName);

            if (file == null)
            {
                return FileNotFound(fileName);
            }

            if (isEncrypting)
            {
                file.encrypted = true;
                file.password = args[3];
                return CreateNode($"File Encrypted: {fileName} is now encrypted with the password {args[3]}.\n");
            }
            else if (isDecrypting)
            {
                if (!file.encrypted)
                {
                    return CreateNode($"Decryption Error: {fileName} is not encrypted.\n");
                }

                if (args[2] == file.password)
                {
                    file.encrypted = false;
                    return CreateNode($"File Decrypted: {fileName} is now decrypted.\n");
                }
                else
                {
                    return CreateNode($"Incorrect Password: {args[2]} is incorrect.\n");
                }
            }

            return CreateNode("Unknown error occurred.\n");

            // Internal method for creating TerminalNode
            TerminalNode CreateNode(string message)
            {
                var node = ScriptableObject.CreateInstance<TerminalNode>();
                node.displayText = message;
                node.clearPreviousText = true;
                return node;
            }
        }

        // Create File
        private static TerminalNode Touch(string filename)
        {
            // Check if the file already exists
            if (files.Any(file => file.name == filename))
            {
                TerminalNode invalidName = ScriptableObject.CreateInstance<TerminalNode>();
                invalidName.displayText = $"Error: {filename} already exists.\n";
                invalidName.clearPreviousText = true;
                return invalidName;
            }

            // Otherwise create the file
            files.Add(new FileCommands.File(filename));

            TerminalNode fileCreated = ScriptableObject.CreateInstance<TerminalNode>();
            fileCreated.displayText = $"File Created: {filename} has been created and added to the desktop.\n";
            fileCreated.clearPreviousText = true;
            return fileCreated;
        }

        // Print File
        private static TerminalNode Cat(string filename)
        {
            if (IsEncrypted(filename))
                return FileEncrypted(filename);
            
            // Check if the file exists
            if (files.Any(file => file.name == filename))
            {
                TerminalNode printFileContents = ScriptableObject.CreateInstance<TerminalNode>();
                printFileContents.displayText = $"{files.FirstOrDefault(file => file.name == filename).content}\n";
                printFileContents.clearPreviousText = true;
                return printFileContents;
            }

            return FileNotFound(filename);
        }

        // TODO ADD rm -rf / as an easter egg
        // Delete File
        private static TerminalNode Remove(string filename)
        {
            // Check if the file exists
            if (files.Any(file => file.name == filename))
            {
                files.Remove(files.FirstOrDefault(file => file.name == filename));

                TerminalNode removeFile = ScriptableObject.CreateInstance<TerminalNode>();
                removeFile.displayText = $"File Removed: {filename} has been deleted.\n";
                removeFile.clearPreviousText = true;
                return removeFile;
            }

            return FileNotFound(filename);
        }

        // When opening a file in nano, this will ensure that the current contents are also displayed
        private static void Terminal_LoadNewNode(On.Terminal.orig_LoadNewNode orig, Terminal self, TerminalNode node)
        {
            node.maxCharactersToType = 1000;

            orig(self, node);

            // Nano Is Open
            if (nanoIsOpen && nanoFile != null)
                TerminalApi.TerminalApi.SetTerminalInput(nanoFile.content);
        }

        private static TerminalNode OpenNano(string filename, Terminal self)
        {
            if (IsEncrypted(filename))
                return FileEncrypted(filename);

            if (IsReadOnly(filename))
                return FileReadOnly(filename);

            if (!filename.EndsWith(".txt"))
            {
                TerminalNode fileNotFound = ScriptableObject.CreateInstance<TerminalNode>();
                fileNotFound.displayText = $"Unknown File Type: '{filename}'\n";
                fileNotFound.clearPreviousText = true;
                return fileNotFound;
            }

            // Check if the file exists
            if (files.Any(file => file.name == filename))
            {
                nanoIsOpen = true;
                nanoFile = files.FirstOrDefault(file => file.name == filename);

                TerminalNode printFileContents = ScriptableObject.CreateInstance<TerminalNode>();
                printFileContents.displayText = "  <color=#FFFFFF>GNU  Nano  4.3            file</color>\n";
                printFileContents.clearPreviousText = true;
                printFileContents.isConfirmationNode = true;
                return printFileContents;
            }

            return FileNotFound(filename);
        }

        private static TerminalNode CloseNano(string text)
        {
            // Nano Is Open
            if (nanoIsOpen && nanoFile != null)
            {
                nanoFile.content = text;

                VanillaAddonsBase.Instance.mls.LogInfo($"VanillaAddons Close Nano");

                TerminalNode fileContentSaved = ScriptableObject.CreateInstance<TerminalNode>();
                fileContentSaved.displayText = $"File Saved: {nanoFile.name} has been saved to the desktop.\n";
                fileContentSaved.clearPreviousText = true;

                nanoFile = null;
                nanoIsOpen = false;

                return fileContentSaved;
            }

            TerminalNode nanoError = ScriptableObject.CreateInstance<TerminalNode>();
            nanoError.displayText = $"Error: An Error occured when opening file with Nano.\n";
            nanoError.clearPreviousText = true;
            return nanoError;
        }
    }
}
