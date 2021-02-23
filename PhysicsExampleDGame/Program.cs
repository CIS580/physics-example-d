using System;

namespace PhysicsExampleDGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new PhysicsExampleDGame())
                game.Run();
        }
    }
}
