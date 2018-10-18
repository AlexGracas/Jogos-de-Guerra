using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerra
{
    public class CommandManager
    {

        Stack<Command> Commands { get; set; }
         = new Stack<Command>();
        Stack<Command> RedoCommands { get; set; }
        = new Stack<Command>();
        public void Execute(Command command)
        {
            command.Execute();
            this.Commands.Push(command);
            this.RedoCommands.Clear();
        }

        public void Undo()
        {
            Command cmd = this.Commands.Pop();
            cmd.Undo();
            this.RedoCommands.Push(cmd);
        }

        public void Redo()
        {
            Command cmd = this.RedoCommands.Pop();
            cmd.Execute();
            this.Commands.Push(cmd);
        }
    }
}
