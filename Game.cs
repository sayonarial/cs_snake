using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake_Pro_Ver
{
    public class Game
    {

        // Game properties

        int sizeX, sizeY; //game window  size
        int Speed; //game speed
        int Arena; //selected arena


        //Stats
        int currentScore = 0;
        int bestScore = 0;
        int pressCounts = 0;

        //Menus
        Menu StartMenu = new Menu("Main Menu", true);
        Menu EndMenu = new Menu("Game Over");

        //TImers


        Collisions Collisions = new Collisions();
        Snake Snake;
        Food Food = new Food(foodChar:'O');
        FoodCourt FoodCourt = new FoodCourt('O');
        Figure FreeSpace = new Figure(' '); //space for placing apples or figures

        string nothingWasPressed = "Дело не сдвинется с места, если ничего не предпринимать";

        enum  Status
        {
            GAMING,
            START_MENU,
            PAUSE,
            END
            
        } 
        Status GameStatus = Status.START_MENU;  //Game status start screen by default

        

        List<string> Arenas = new List<string>{ 
            "Classic",
            "Sprint",
            "Rectangles"
        };
        

        List<string> Speeds = new List<string>
        {
            "Slow",
            "Normal",
            "Fast"
        };
        

        public Game(int sizeX, int sizeY) //New game window
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;

            Arena = 0; //default arena 
            Speed = 1; //Normal speed
            //create a main menus
            StartMenu.AddItems(new List<MenuItem> {
                    new MenuItem("Start Game", () => StartGame()),
                    new MenuItem("Arena", () => ChangeArena()),
                    new MenuItem("Speed", () => ChangeSpeed()),
                    new MenuItem("Exit", () => Environment.Exit(0))
                    });
            EndMenu.AddItems(new List<MenuItem> {
                    new MenuItem("Start Again", () => StartGame()),
                    new MenuItem("To Main Menu", () => GameStatus = Status.START_MENU),
                    new MenuItem("Exit", () => Environment.Exit(0))
                    });


        }

        public void StartGame() //creates new arena from settings
        {
            Console.Clear(); //clear all from screen

            GameStatus = Status.GAMING; //set Game status to gaming
            currentScore = 0; //reset current score
            pressCounts = 0; //reset press counts

            CreateArena(Arena);

            
        }
        public void GameLoop() //main loop for game
        {
            
            //If its first run show menu
            //If game is Paused show pause resume menu
            //if game Over show Game ower screen
            //if game is still going, show game frame

            switch (GameStatus)
            {
                case Status.GAMING:
                    //Show Game frame
                    RenderFrame();
                    break;
                case Status.END:
                    //Show Game Over screen
                    EndMenu.Show();
                    break;
                case Status.PAUSE:
                    //Show Pause Screen
                    break;
                case Status.START_MENU:
                    //Show start Menu
                    StartMenu.Show();
                    break;
            }

        }

        void RenderFrame() {

            //sleep delay (it takes me more time to understand async methods)
            switch (Speed)
            {
                case 0: Thread.Sleep(130); break;
                case 1: Thread.Sleep(75); break;
                case 2: Thread.Sleep(30); break;

            }
            

            //add snake to free space
            FreeSpace.Add(Snake);
            // move snake
            Snake.Move();
            // remove from free space
            FreeSpace.Remove(Snake);

            if (Snake.IsHit(Food) == true)
            {
                Snake.Add(new Pixel(Food.x,Food.y));
                Food.Spawn(FreeSpace);
                currentScore++;
            }
            else if(Snake.IsHit(FoodCourt) == true)
            {
                Snake.Add(new Pixel(Snake.head.x, Snake.head.y));
                FoodCourt.Remove(Snake.head);
                currentScore++;
            }
            else if (Snake.IsHitItself())
            {
                GameOver("И такое бывает...");
            }
            else if (Snake.IsHit(Collisions))
            {

                if (Snake.GetPressCounts() == 0) GameOver(nothingWasPressed);
                GameOver("Стены лбом не прошибешь");
            }
            else { 
                Snake.Show();
                GamingStats();
            }
                    
        }

        void GamingStats()
        {
            Console.SetCursorPosition(3,sizeY+1);
            Console.Write($"Score:  {currentScore}");
        }
        public void Pause() //pause screen
        {

        }
        
        static int GetBestScoreFromFile()
        {
            return 0;
        }

        void SaveStatsAndSettings()
        {
            //Save stats and settings to text file
            
            
        }



        void GameOver(string message) {

            Console.Clear();
            Console.SetCursorPosition(sizeX / 2 - message.Length / 2, 5);
            Console.Write(message);
            string stats = $"Your score: {currentScore}";
            Console.SetCursorPosition(sizeX / 2 - stats.Length / 2, 7);
            Console.Write(stats);
            if (currentScore <= 100)
            {
                Terminal.CenteredText("Если набрать больше 100 очков - покажут мультик",sizeX,9);
                Terminal.CenteredText("Но это не точно", sizeX,  10);
            }
            else
            {
                Terminal.CenteredText("Ого, круто! Но мультика не будет.", sizeX,  10);
            }
            GameStatus = Status.END;

        }

        void ChangeArena()
        {
            Menu Arena = new Menu("Select Arena");
            Arena.AddItems(new List<MenuItem> {
                    
                    new MenuItem("Classic", () => SetArena(0)),
                    new MenuItem("Sprint", () => SetArena(1)),
                    new MenuItem("Rectangles", () => SetArena(2))
                    });
            do
            {
                Arena.Show();
            } while (Arena.EnterIsPressed == false);
        }
        void SetArena(int arenaIndex)
        {
            Arena = arenaIndex;
        }
        void ChangeSpeed()
        {
            Menu SpeedMenu = new Menu("Select Speed");
            SpeedMenu.AddItems(new List<MenuItem> {

                    new MenuItem("Slow", () => SetSpeed(0)),
                    new MenuItem("Normal", () => SetSpeed(1)),
                    new MenuItem("Fast", () => SetSpeed(2))
                    });
            do
            {
                SpeedMenu.Show();
            } while (SpeedMenu.EnterIsPressed == false);
        }
        void SetSpeed(int ind)
        {
            Speed = ind;
        }

        string GetArenaName()
        {
            return Arenas[Arena];
        }

        void CreateArena(int Arena)
        {


            //make simple frame
            Figure Frame = new Figure('▓');
            Frame.CreateFrame(new Pixel(0, 0), new Pixel(sizeX, sizeY));
            Frame.CreateFrame(new Pixel(0, sizeY), new Pixel(sizeX, sizeY + 2));
            //add frames to collision list
            Collisions.Add(Frame);
            //Create Available space for placing apples
            FreeSpace.CreateBox(new Pixel(0, 0), new Pixel(sizeX, sizeY));
            //Exclude frame  from free space
            FreeSpace.Remove(Frame);
            //add snake to game
            Snake = new Snake(new Pixel(3, sizeY / 2), 2, '+');
            //exclude snake from available space
            FreeSpace.Remove(Snake);

            //////show free space
            ////Console.BackgroundColor = ConsoleColor.Green;
            ////FreeSpace.Draw();
            ////Console.BackgroundColor = ConsoleColor.Black;

            switch (Arena)
            {
                case 0: //classic frame with snake
                    
                    //add first food in available space
                    Food.Spawn(FreeSpace);

                    break;
                case 1: //Sprint - a lot of food
                    //spawn a lot of food
                    FoodCourt.SpawnFood(FreeSpace,150);

                    break;
                case 2: //rectangles spawn
                    Figure Rectangles = new Figure('▓');
                    int gridW = sizeX / 8;
                    int gridH = sizeY / 8;
                    Rectangles.CreateBox(new Pixel(0,0), gridW * 2, gridH* 2);
                    Rectangles.CreateBox(new Pixel(0, gridH*6+1), gridW * 2, gridH * 2);
                    Rectangles.CreateBox(new Pixel(gridW*6+1, gridH * 6+1), gridW * 2, gridH * 2);
                    Rectangles.CreateBox(new Pixel(gridW * 6+1, 0), gridW * 2, gridH * 2);
                    Rectangles.CreateBox(new Pixel(gridW *3 + 1, gridH *3+1), gridW * 2, gridH * 2);

                    Collisions.Add(Rectangles);
                    FreeSpace.Remove(Rectangles);

                    //add first food in available space
                    Food.Spawn(FreeSpace);

                    break;

                default:
                    Console.WriteLine("Undefined game mode!");
                    break;
            }

            //Show all collisions
            Collisions.Show();
        }
    }

    
}
