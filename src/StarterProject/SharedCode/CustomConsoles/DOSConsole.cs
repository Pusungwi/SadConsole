﻿#if MONOGAME
using Microsoft.Xna.Framework;
#elif SFML
using Point = SFML.System.Vector2i;
#endif

using System;

namespace StarterProject.CustomConsoles
{
    class DOSConsole: SadConsole.Consoles.Console
    {
        public string Prompt { get; set; }

        private InputHandling.ClassicConsoleKeyboardHandler _keyboardHandlerObject;

        // This console domonstrates a classic MS-DOS or Windows Command Prompt style console.
        public DOSConsole()
            : base(80, 25)
        {
            this.IsVisible = false;

            // This is our cusotmer keyboard handler we'll be using to process the cursor on this console.
            _keyboardHandlerObject = new InputHandling.ClassicConsoleKeyboardHandler();

            // Assign our custom handler method from our handler object to this consoles keyboard handler.
            // We could have overridden the ProcessKeyboard method, but I wanted to demonstrate how you
            // can use your own handler on any console type.
            KeyboardHandler = _keyboardHandlerObject.HandleKeyboard;

            // Our custom handler has a call back for processing the commands the user types. We could handle
            // this in any method object anywhere, but we've implemented it on this console directly.
            _keyboardHandlerObject.EnterPressedAction = EnterPressedActionHandler;

            // Enable the keyboard and setup the prompt.
            CanUseKeyboard = true;
            VirtualCursor.IsVisible = true;
            Prompt = "Prompt> ";


            // Startup description
            ClearText();
            VirtualCursor.Position = new Point(0, 24);
            VirtualCursor.Print("Try typing in the following commands: help, ver, cls, look. If you type exit or quit, the program will end.").NewLine().NewLine();
            _keyboardHandlerObject.VirtualCursorLastY = 24;
            TimesShiftedUp = 0;

            VirtualCursor.Print(Prompt);
        }

        public void ClearText()
        {
            Clear();
            VirtualCursor.Position = new Point(0, 24);
            _keyboardHandlerObject.VirtualCursorLastY = 24;
        }

        private void EnterPressedActionHandler(string value)
        {
            if (value.ToLower() == "help")
            {
                VirtualCursor.NewLine().
                              Print("  Advanced Example: Command Prompt - HELP").NewLine().
                              Print("  =======================================").NewLine().NewLine().
                              Print("  help      - Display this help info").NewLine().
                              Print("  ver       - Display version info").NewLine().
                              Print("  cls       - Clear the screen").NewLine().
                              Print("  look      - Example adventure game cmd").NewLine().
                              Print("  exit,quit - Quit the program").NewLine().
                              Print("  ").NewLine();
            }
            else if (value.ToLower() == "ver")
                VirtualCursor.Print("  SadConsole for MonoGame and SFML").NewLine();

            else if (value.ToLower() == "cls")
                ClearText();

            else if (value.ToLower() == "look")
                VirtualCursor.Print("  Looking around you discover that you are in a dark and empty room. There is a computer monitor in front of you and Visual Studio is opened, waiting for your next command.").NewLine();

            else if (value.ToLower() == "exit" || value.ToLower() == "quit")
                Environment.Exit(0);

            else
                VirtualCursor.Print("  Unknown command").NewLine();
        }
    }
}
