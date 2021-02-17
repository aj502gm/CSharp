using System;
using System.IO;
using System.Threading;

namespace BattleShipSimulator {
    /*
      * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/ref
      * http://www.blackwasp.co.uk/commaseparatedtoarray.aspx
      * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-read-from-a-text-file/    */
    class Program {
        //players and match info
        static private int rounds = 0, hitsPlayer1 = 0, hitsPlayer2 = 0;

        public static bool noShipsLeft(int[,] matrizJugadorX) {
            bool ships = false;
            for (int i = 0; i < matrizJugadorX.GetLength(0); i++) {
                for (int j = 0; j < matrizJugadorX.GetLength(1); j++) {
                    if (matrizJugadorX[i, j] == 1) ships = true;  
                }
            }
            return ships; //if true means there is at least one ship alive
        }
        public static void fillMatrix(int[,] matrizJugadorX) {
            for (int i = 0; i < matrizJugadorX.GetLength(0); i++) {
                for (int j = 0; j < matrizJugadorX.GetLength(1); j++) {
                    matrizJugadorX[i, j] = 0;
                }
            }
        }
        public static void showMatrix(int[,] matrizJugadorX, bool bandera) {
            int jj = 0;
            Console.Write("    ");
            for (int i = 0; i < matrizJugadorX.GetLength(0); i++) {
                for (int j = 0; j < matrizJugadorX.GetLength(1); j++) {
                    while (j == 0 && jj < matrizJugadorX.GetLength(1)) for (jj = 0; jj < matrizJugadorX.GetLength(1); jj++) Console.Write(jj + " ");
                    if (j == 0) Console.Write("\n" + (i) + "  ");
                    if (bandera) {
                        if (matrizJugadorX[i, j] == 0) {
                            Console.Write(" -");
                        } else if (matrizJugadorX[i, j] == 1) {
                            Console.Write(" !");
                        } else if (matrizJugadorX[i, j] == 2) Console.Write(" *");
                    } else {
                        if (matrizJugadorX[i, j] == 2) {
                            Console.Write(" *");
                        } else Console.Write(" -");
                    }
                }
                Console.WriteLine("");
            }
        }
        public static void readConfigFile(string nameFile, ref int[,] matrizJugadorX) {
            string[] lineasTxt = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory) + "\\" + nameFile);
            string config = "";
            int ships = 0, temp = 0;
            for (int i = 0; i < lineasTxt.Length; i++) {
                config = lineasTxt[i].Replace(" ", String.Empty);
                string[] valores = config.Split(",");
                if (i == 0)//board dimensions
                {
                    matrizJugadorX = new int[Convert.ToInt32(valores[1]), Convert.ToInt32(valores[0])]; //5<X<10
                    fillMatrix(matrizJugadorX);
                } else if (i == 1) //at least one ship in the config file
                  {
                    ships = Convert.ToInt32(config);
                } else //Ship location -------> x,y,<orientation>,size
                  {
                    if (Convert.ToInt32(valores[0]) < 10 && Convert.ToInt32(valores[1]) < 10) //no ships out of bounds
                    {
                        int x = Convert.ToInt32(valores[0]), y = Convert.ToInt32(valores[1]);
                        int width = Convert.ToInt32(valores[3]);
                        if (valores[2].ToLower() == "h") {
                            for (int i2 = 0; i2 < width; i2++) {
                                if (matrizJugadorX[x, y] != 1) 
                                {
                                    matrizJugadorX[x, y] = 1;
                                    y++;
                                } else throw new Exception("Error");
                            }
                            temp++;
                        } else if (valores[2].ToLower() == "v") {
                            for (int i2 = 0; i2 < width; i2++) {
                                if (matrizJugadorX[x, y] != 1) {
                                    matrizJugadorX[x, y] = 1;
                                    x++;
                                } else throw new Exception("Error"); //not valid orientation or location 
                            }
                            temp++;
                        } else throw new Exception("Error");
                    } else throw new IndexOutOfRangeException("Error");//ship out of bounds
                }
            }
            if (ships != temp) throw new OverflowException("Error");
        }

        static void Main(string[] args) {
            int[,] player1Board = new int[1, 1];
            int[,] player2Board = new int[1, 1];
            int x, y;
            string keepPlaying;
            try {
                readConfigFile("player1.txt", ref player1Board);
                readConfigFile("player2.txt", ref player2Board);
                if (player1Board.GetLength(0) == player2Board.GetLength(0) && player1Board.GetLength(1) == player2Board.GetLength(1)) {
                    while (true) {
                        if (!noShipsLeft(player1Board)) break;
                        rounds++;
                        Console.WriteLine("Player 1");
                        showMatrix(player1Board, true);
                        Console.WriteLine("Player 2");
                        showMatrix(player2Board, false);
                        Console.WriteLine("----READY PLAYER 1-----");
                        Console.WriteLine("     X location to attack: ");
                        x = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("     Y location to attack: ");
                        y = Convert.ToInt32(Console.ReadLine());
                        if (player2Board[x, y] == 1) 
                        {
                            player2Board[x, y] = 2;
                            hitsPlayer1++;
                        }
                        if (!noShipsLeft(player2Board)) break;
                        Thread.Sleep(2000); 
                        Console.WriteLine("Player 2");
                        showMatrix(player2Board, true);
                        Console.WriteLine("Player 1");
                        showMatrix(player1Board, false);
                        Console.WriteLine("----READY PLAYER 2-----");
                        Console.WriteLine("     X location to attack: ");
                        x = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("     y location to attack: ");
                        y = Convert.ToInt32(Console.ReadLine());
                        if (player1Board[x, y] == 1) 
                        {
                            player1Board[x, y] = 2;
                            hitsPlayer2++;
                        }
                        if (!noShipsLeft(player1Board)) break;
                        Thread.Sleep(2000); 
                        Console.WriteLine("Keep playing? y/n");
                        if ((keepPlaying = Console.ReadLine()) == "n") break;
                    }
                } else Console.WriteLine("ERROR! THE SIZE OF THE BOARDS DOES NOT MATCH");
            } catch (IndexOutOfRangeException IE) {
                Console.WriteLine("INVALID COORDINATES OF ATTACK!");
            } catch (OverflowException oe) {
                Console.WriteLine("THE NUMBER OF BOATS ENTERED DOES NOT MATCH");
            } catch (FormatException fe) {
                Console.Write("INVALID TYPE OF DATA IN THE CONFIGURATIONS");
            } catch (Exception ex) {
                Console.WriteLine("INVALID ORIENTATION OR POSITION");
            } finally {
                Console.WriteLine("\n---- GAME SUMMARY ---- \nRounds played: " + rounds + " \nHits Player 1: " + hitsPlayer1 + " \nHits Player 2: " + hitsPlayer2);
            }
        }
    }
}
