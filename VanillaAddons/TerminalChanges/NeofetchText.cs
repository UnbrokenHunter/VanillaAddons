using System.Threading.Tasks;

namespace VanillaAddons.TerminalChanges
{
    public class NeofetchText
    {
        private static int uptimeMinutes = 0;
        private static bool isTrackingUptime = false;

        static void init()
        {
            if (!isTrackingUptime)
            {
                isTrackingUptime = true;
                Task.Run(async () =>
                {
                    while (isTrackingUptime)
                    {
                        await Task.Delay(60000); // Wait for 1 minute
                        uptimeMinutes++;
                    }
                });
            }
        }

        public static string GetNeofetch()
        {
            init();

            int packageCount = GetPackageCount();
            string uptimeText = FormatUptime(uptimeMinutes);
            string colorBlocks = GenerateColorBlocks();

            return
                "         <color=#76FFEF>++++++++++</color>            <color=#76FFEF>root</color>@<color=#76FFEF>terminal</color>\r\n" +
                "      <color=#76FFEF>+=--=++++++==-=+</color>         -------------\r\n" +
                "    <color=#76FFEF>=-=++          ++=-=</color>       <#CE00CC>OS:</color> FORTUNE-9\r\n" +
                $"  <color=#76FFEF>+-=++      -*-     *+=-+</color>     <#CE00CC>Uptime:</color> {uptimeText}\r\n" +
                $" <color=#76FFEF>+-++     +==+++-++    ++-+</color>    <#CE00CC>Packages:</color> {packageCount}\r\n" +
                "<color=#76FFEF>+-++     =-+     +-+    ++-+</color>   \r\n" +
                $"<color=#76FFEF>==+     +-=      +-=+    +==</color>   {colorBlocks}\r\n" +
                "<color=#76FFEF>-++     *==+     +--+    ++-</color>   \r\n" +
                "<color=#76FFEF>-++      ++-++++==--+    *+-</color>\r\n" +
                "<color=#76FFEF>==+          ++* +==*    +==</color>\r\n" +
                "<color=#76FFEF>+-++            +==+    ++-+</color>\r\n" +
                " <color=#76FFEF>+-++    +=++++=-+     ++-+</color>\r\n" +
                "  <color=#76FFEF>+-=+*    ++++      *+=-+</color>\r\n" +
                "    <color=#76FFEF>=-=++          ++=-=</color>  \r\n" +
                "      <color=#76FFEF>+=--++++++++=-=++</color>  \r\n" +
                "        <color=#76FFEF>++++++++++++</color> \r\n\r\n\r\n";

            string FormatUptime(int minutes)
            {
                if (minutes < 60)
                {
                    return minutes != 1 ? $"{minutes} mins" : "1 min";
                }
                else if (minutes < 120)
                {
                    return minutes % 60 != 1 ? $"1 hour, {minutes % 60} mins" : "1 hour, 1 min";
                }
                else
                {
                    return minutes % 60 != 1 ? $"{minutes / 60} hours, {minutes % 60} mins" : $"{minutes / 60} hours, 1 min";
                }
            }

            string GenerateColorBlocks()
            {
                return "<mark=#000000><color=black>__</color></mark><mark=#CC0000><color=#CC0000>__</color></mark>" +
                       "<mark=#00CD00><color=#00CD00>__</color></mark><mark=#CDCD00><color=#CDCD00>__</color></mark>" +
                       "<mark=#0000CD><color=#0000CD>__</color></mark><mark=#CE00CC><color=#CE00CC>__</color></mark>" +
                       "<mark=#01CDCF><color=#01CDCF>__</color></mark><mark=#E5E5E5><color=#E5E5E5>__</color></mark>";
            }

            int GetPackageCount()
            {
                return BepInEx.Bootstrap.Chainloader.PluginInfos.Count;
            }
        }

    }
}
