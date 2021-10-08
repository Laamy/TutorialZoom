using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TutorialZoom
{
    class Program
    {
        static void Main(string[] args)
        {
            MCM.openGame();
            MCM.openWindowHost();

            // 0x0411FEC8 is the base address other then mc.windows.exe
            ClientInstance = MCM.baseEvaluatePointer(0x0411FEC8, MCM.ceByte2uLong("0 18"));

            //Console.WriteLine(ClientInstance.ToString("X"));

            new Keymap(); // init keymap

            Keymap.keyEvent += mcKeyEvent;

            while (true) // pause console
                Thread.Sleep(1);
        }

        public static ulong ClientInstance = 0x0;

        private static void mcKeyEvent(object sender, KeyEvent e)
        {
            if (ClientInstance == 0x0) return;

            ulong LocalPlayer = MCM.evaluatePointer(ClientInstance, MCM.ceByte2uLong("B8 0")); // dont base evl the local player
            ulong fieldOfViewAddr = LocalPlayer + 0x1140;

            //Console.WriteLine(LocalPlayer.ToString("X"));

            if (e.vkey == VKeyCodes.KeyDown)
            {
                if (e.key == System.Windows.Forms.Keys.C)
                {
                    MCM.writeFloat(fieldOfViewAddr, 0.2f);
                }
            }

            if (e.vkey == VKeyCodes.KeyUp)
            {
                if (e.key == System.Windows.Forms.Keys.C)
                {
                    MCM.writeFloat(fieldOfViewAddr, 1f);
                }
            }
        }
    }
}
