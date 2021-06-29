using Microsoft.ClearScript;
using System.Windows.Forms;

namespace SSPM
{
    public static class SSPM
    {
        public static string jsSource { get; } = Properties.Resources.sppm;

        public static void StrokesPlusInitStaticPlugin(ScriptEngine e)
        { 
            try
            {
                e.Execute(jsSource);
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Message, "SSPM Plugin Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
