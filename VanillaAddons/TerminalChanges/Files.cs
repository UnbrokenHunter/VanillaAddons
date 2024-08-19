using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerminalApi.Classes;
using VanillaAddons.TerminalChanges.Patches;
using static TerminalApi.TerminalApi;

namespace VanillaAddons.TerminalChanges
{
    public class Files
    {
        // TODO Make Sync Across Multiplayer
        public List<File> files = new List<File>();

        public class File
        {
            public string name;
            public string content;

            public bool isReadOnly = false;
            public bool encrypted = false;

            public File(string name)
            {
                this.name = name;
            }
        }

        public Files() 
        {
            FilenamesPatch.Init(files);

            DefaultFiles();

            List();
        }

        private void DefaultFiles()
        {
            File passwords = new File("passwords.txt");
            passwords.encrypted = true;
            passwords.isReadOnly = true;
            files.Add(passwords);

            File shipMaintenance = new File("maintenance.log");
            shipMaintenance.content =
                "Log Date: Aug 25, 1968\n" +
                "Minor damage to rear thruster. Will fix tmmr.\n" +
                "\n" +
                "Log Date: Aug 26, 1968\n" +
                "Damage was more severe than expected. Took a few hrs but it's good now.\n" +
                "\n" +
                "Log Date: Sep 10, 1968\n" +
                "Ship door locking shut occasionally. Otherwise ship is fine.\n" +
                "\n" +
                "Log Date: Sep 22, 1968\n" +
                "Lightning struck the ship. It tripped the breaker, but the lights turned back on, so everything seems fine.\n" +
                "\n" +
                "Log Date: Oct 2, 1968\n" +
                "Ship door keeps locking up. Might try to make it battery powered or smthg.\n" +
                "\n" +
                "Log Date: Oct 15, 1968\n" +
                "Installed new Signal Translator. Autopilot acted up during installation. Will monitor for further issues.\n" +
                "\n" +
                "Log Date: Oct 30, 1968\n" +
                "Main pressure door malfunctioning. Suspect interference with new systems. Will need to recalibrate.\n" +
                "\n" +
                "Log Date: Nov 5, 1968\n" +
                "Ship autopilot initiated uncommanded departure sequence. Manual override failed. Incident logged for review.\n" +
                "\n";
            shipMaintenance.isReadOnly = true;
            files.Add(shipMaintenance);

            File welcome = new File("welcome.txt");
            welcome.content =
                "         <color=#76FFEF>:: WELCOME ::</color>\r\n" +
                "\r\n" +
                "         <color=#FFFF00>:: DATE :: [SYSTEM ERROR]</color>\r\n" +
                "\r\n" +
                "Employee,\r\n" +
                "\r\n" +
                "You have been assigned to the Company Cruiser. Your role is to execute the tasks outlined by the Company.\r\n" +
                "\r\n" +
                "Mission: Gather materials. Report findings. Maintain operational efficiency.\r\n" +
                "\r\n" +
                "Your actions are monitored to ensure alignment with Company objectives. All communication and behavior are subject to observation. The Company values precision and compliance.\r\n" +
                "\r\n" +
                "Proceed with your duties as instructed. The Company’s purpose is beyond individual comprehension—focus on your assigned tasks. Any deviations from standard procedure will be addressed accordingly.\r\n" +
                "\r\n" +
                "Your contribution is essential. The Company relies on each employee to fulfill their role without question.\r\n" +
                "\r\n" +
                "         <color=#FF6347>:: OBSERVATION ACTIVE ::</color>\r\n" +
                "\r\n" +
                "         <color=#76FFEF>:: END OF MESSAGE ::</color>\r\n";
            welcome.isReadOnly = true;
            files.Add(welcome);

            File errorLogs = new File("error_logs.sys");
            errorLogs.content =
                "[ERROR 09262068-01] Unresolved system conflict detected in environmental controls.\n" +
                "[ERROR 09282068-02] Autopilot deviation detected. Cause: Unknown.\n" +
                "[ERROR 10011968-03] Communication link to Company HQ disrupted. Signal interference suspected.\n" +
                "\n" +
                "System reboot initiated... Reboot failed.\n" +
                "\n";
            errorLogs.isReadOnly = true;
            files.Add(errorLogs);

            File directive = new File("comp_directive_7.txt");
            directive.content =
                "Company Directive 7\n" +
                "Issued: Sep 10, 1968\n" +
                "\n" +
                "All crews assigned to 68-Artifice and 85-Rend are to adhere to the following protocol:\n" +
                "- Do not engage with local wildlife.\n" +
                "- Avoid sectors marked as 'Restricted'.\n" +
                "- Report any unusual findings immediately.\n" +
                "- Maintain focus on primary objectives. No deviation is permitted.\n" +
                "- Ensure all interactions with the Autopilot system are logged. Anomalous behavior must be reported directly to Company HQ.\n" +
                "\n" +
                "Compliance is mandatory.\n";
            directive.isReadOnly = true;
            files.Add(directive);

            File blueprint = new File("ship_blueprint.img");
            blueprint.content =
                "Ship Blueprint - Model X-7 Cruiser\n" +
                "Version: 1.2\n" +
                "\n" +
                "[Image Data Corrupted]\n" +
                "Note: Includes stock components such as Terminal, Monitors, and Main Pressure Door. Upgrades like Teleporter and Signal Translator available for purchase. Sections 4B and 7F are restricted to authorized personnel only.\n";
            blueprint.isReadOnly = true;
            files.Add(blueprint);

            File autopilotReport = new File("autopilot_report.txt");
            autopilotReport.content =
                "Autopilot System Report - Sep 20, 1968\n" +
                "Status: Active\n" +
                "Recent Activity: Moon 85-Rend - Minor course deviation detected. Manual override attempt unsuccessful. Cause: Unknown.\n" +
                "\n" +
                "Status: Active\n" +
                "Recent Activity: Moon 68-Artifice - Sudden drop in altitude before stabilization. Crew unaware of incident. Cause: External interference suspected.\n" +
                "\n" +
                "Status: Active\n" +
                "Recent Activity: Moon 71-Gordion - Successful docking. Autopilot initiated undocking sequence without command input. No crew onboard. Incident logged for further review.\n" +
                "\n";
            autopilotReport.isReadOnly = true;
            files.Add(autopilotReport);
        }

        // Display Files
        void List()
        {
            AddCommand("ls", new CommandInfo
            {
                Category = "files",
                Description = "Prints the working directory.",
                DisplayTextSupplier = OnListCommand
            },
            clearPreviousText: true);
            
            string OnListCommand()
            {
                if (files.Count == 0)
                {
                    return "No files in the directory.\n";
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Files:");
                files.ForEach(file => sb.AppendLine(file.name));
                return $"{sb.ToString().Trim()}\n\n";
            }
        }

    }
}
