using System;

namespace Bartolini.Liam._3H.CrociPuzzle
{
    class Program
    {
        static int contP = 0;
        //dichiaro una struttura per colorare le parole trovate
        public struct ValoriParole
        {
            public int R;
            public int C;
            public int uR;
            public int uC;
            public string M;

            public void Valori(int riga, int colonna, int fR, int fC, string modo)
            {
                R = riga;
                C = colonna;
                uR = fR;
                uC = fC;
                M = modo;
            }
        }
        static ValoriParole[] p = new ValoriParole[13];

        static void Main(string[] args)
        {

            Console.WriteLine("Cruci puzzle Bartolini Liam 3H");

            //scrivo le parole da cercare
            string[] parole = new string[] { "NEANCHE", "MAI", "TROPPO", "OGGI", "CERTAMENTE", "FORSE", "STAMANE", "DAVANTI", "BENE"};

            //scolpisco la matrice con le parole nascoste
            char[,] puzzle =
            {
                {'S', 'S', 'I', 'F', 'C', 'Q', 'Y', 'K', 'Q', 'Y'},
                {'E', 'T', 'N', 'O', 'E', 'M', 'A', 'I', 'M', 'F'},
                {'P', 'O', 'A', 'S', 'R', 'A', 'T', 'W', 'G', 'X'},
                {'J', 'A', 'R', 'M', 'T', 'N', 'T', 'N', 'J', 'B'},
                {'C', 'O', 'D', 'E', 'A', 'B', 'E', 'D', 'J', 'P'},
                {'F', 'Q', 'X', 'V', 'M', 'N', 'O', 'J', 'B', 'I'},
                {'N', 'H', 'A', 'G', 'E', 'H', 'E', 'U', 'G', 'H'},
                {'Y', 'D', 'B', 'B', 'N', 'W', 'Q', 'G', 'E', 'H'},
                {'N', 'Y', 'V', 'D', 'T', 'R', 'O', 'P', 'P', 'O'},
                {'F', 'R', 'Y', 'N', 'E', 'A', 'N', 'C', 'H', 'E'}
            };

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Ricerca orizzontale: ");
            RicercaNelleRighe(puzzle, parole, p);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Ricerca orizzontale inversa: ");
            RicercaNelleRigheRevers(puzzle, parole, p);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Ricerca verticale: ");
            RicercaNelleColonne(puzzle, parole, p);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Ricerca verticale inversa: ");
            RicercaNelleColonneRevers(puzzle, parole, p);
            

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Ricerca obliqua inversa: ");
            RicercaNelleObliqueRevers(puzzle, parole, p);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Ricerca obliqua: ");
            RicercaNelleOblique(puzzle, parole, p);

            Console.ResetColor();

            //chiamo il metodo per la matrice dietro, poi la comparto con la matrice puzzle e i valori diversi da '0' li coloro nella matrice puzzle
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            Random random = new Random();
            char[,] matrice = BehindMatrix(p);
            //int contC = 0;
            for (int r = 0; r < puzzle.GetLength(0); r++)
            {
                for (int c = 0; c < puzzle.GetLength(1); c++)
                {
                    if (matrice[r, c] == '1')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("   " + puzzle[r, c]);
                        Console.ResetColor();
                    }
                    else
                        Console.Write("   " + puzzle[r, c]);
                }
                Console.WriteLine("\n");
            }
        }

        static void MatriceInizializzata(char[,] puzzle)
        {
            //funzione per inizializzare la matrica con tutti 0
            for (int riga = 0; riga < 10; riga++)
                for (int colonna = 0; colonna < 10; colonna++)
                    puzzle[riga, colonna] = '0';
        }

        //metodo per fare la matrice dietro da usare come 'stampo' per la prima
        static char[,] BehindMatrix(ValoriParole[] p)
        {
            char[,] matrice = new char[10, 10];
            //contatori per colorare la matrice
            int tmpC, tmpR, tmpUC, tmpUR, i, controlloR, controlloC;
            string tmpM;

            MatriceInizializzata(matrice);

            contP = 0;
            while (contP != 10)
            {
                for (int riga = 0; riga < 10; riga++)
                {
                    for (int colonna = 0; colonna < 10; colonna++)
                    {
                        tmpM = p[contP].M;

                        if (tmpM == "diagonaleInversa")
                        {
                            //coordinate d'inizio parola
                            controlloR = p[contP].R;
                            controlloC = p[contP].C;

                            //coordinate di fine parola
                            tmpUR = p[contP].uR + 1;
                            tmpUC = p[contP].uC + 1;

                            while (controlloC < tmpUC || controlloR < tmpUR)
                            {
                                matrice[controlloR, controlloC] = '1';

                                controlloC++;
                                controlloR++;
                            }
                        }


                        if (tmpM == "diagonale")
                        {
                            //coordinate d'inizio parola
                            controlloR = p[contP].R;
                            controlloC = p[contP].C;

                            //coordinate di fine parola
                            tmpUC = p[contP].uC + 1;
                            tmpUR = p[contP].uR - 1;

                            while (controlloC != tmpUC && controlloR != tmpUR)
                            {
                                matrice[controlloR, controlloC] = '1';

                                controlloC += 1;
                                controlloR -= 1;
                            }
                        }

                        if (tmpM == "riga")
                        {
                            //inizio della parola
                            tmpC = p[contP].C;
                            tmpR = p[contP].R;
                            //fine della parola
                            tmpUC = p[contP].uC;

                            for (i = tmpC; i <= tmpUC; i++)
                                matrice[tmpR, i] = '1';
                        }

                        if (tmpM == "rigaInversa")
                        {
                            //inizio della parola
                            tmpR = p[contP].uR;
                            tmpC = p[contP].uC;

                            //fine parola
                            tmpUC = p[contP].C;

                            for (i = tmpC; i >= tmpUC; i--)
                                matrice[tmpR, i] = '1';
                        }

                        if (tmpM == "verticale")
                        {
                            tmpC = p[contP].C;

                            tmpR = p[contP].R;
                            tmpUR = p[contP].uR;

                            for (i = tmpR; i <= tmpUR; i++)
                                matrice[i, tmpC] = '1';

                        }

                        if (tmpM == "verticaleInversa")
                        {
                            //valori inizio
                            tmpC = p[contP].C;
                            tmpR = p[contP].R;

                            //valori fine
                            tmpUR = p[contP].uR;

                            for (i = tmpR; i >= tmpUR; i--)
                                matrice[i, tmpC] = '1';
                        }
                    }
                    contP++;
                }
            }
            //StampaMatrice(matrice);
            return matrice;
        }

        static void RicercaNelleRighe(char[,] puzzle, string[] parole, ValoriParole[] p)
        {
            int cont, k = 0;
            char[] ricercate = new char[parole.Length];
            char[] cercata = new char[11];

            while (k < parole.Length)
            {
                Array.Clear(ricercate, 0, ricercate.Length);
                ricercate = parole[k].ToCharArray();
                for (int righe = 0; righe < 10; righe++)
                {
                    for (int colonne = 0; colonne < 10; colonne++)
                    {
                        //quando trovo la prima lettera della parola cercata controllo quelle dopo
                        if (puzzle[righe, colonne] == ricercate[0])
                        {
                            //metto i valori della posizione della prima lettera nella struttura
                            p[contP].R = righe;
                            p[contP].C = colonne;
                            p[contP].M = "riga";

                            //imposto contatore e vettore a 0 così quando entro nel ciclo sono liberi
                            cont = 0;
                            Array.Clear(cercata, 0, cercata.Length);
                            for (int i = colonne; i < 10; i++)
                            {
                                cercata[cont] = puzzle[righe, i];

                                //se la stringa della parola trovata è uguale alla stringa cercata allora ritorno in ris
                                if (string.Compare(new string(cercata), new string(ricercate)) == 0)
                                {
                                    p[contP].uC = i;
                                    p[contP].uR = righe;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Find --> " + new string(cercata));
                                    Console.ResetColor();
                                    //stampiamo i valori delle parole
                                    //Console.WriteLine("Valori parola {0} \nR -> {1}\nC -> {2}\nM -> {3}", new string(cercata), p[contP].R, p[contP].C, p[contP].M);
                                    contP++;
                                }

                                cont++;
                            }
                        }
                    }
                }
                //contatore per la condizione del ciclo e delle parola da cercare
                k++;
            }
        }

        static void RicercaNelleColonne(char[,] puzzle, string[] parole, ValoriParole[] p)
        {
            int cont, k = 0;
            char[] ricercate;
            char[] cercata = new char[11];

            while (k < parole.Length)
            {
                ricercate = parole[k].ToCharArray();
                for (int righe = 0; righe < 10; righe++)
                {
                    for (int colonne = 0; colonne < 10; colonne++)
                    {
                        //quando trovo la prima lettera della parola cercata controllo quelle dopo
                        if (puzzle[righe, colonne] == ricercate[0])
                        {
                            p[contP].R = righe;
                            p[contP].C = colonne;
                            p[contP].M = "verticale";

                            //imposto indice e vettore a 0 così quando entro nel ciclo sono liberi
                            cont = 0;
                            Array.Clear(cercata, 0, cercata.Length);
                            for (int i = righe; i < 10; i++)
                            {
                                cercata[cont] = puzzle[i, colonne];
                                //se la stringa della parola trovata è uguale alla stringa cercata allora ritorno in ris
                                if (string.Compare(new string(cercata), new string(ricercate)) == 0)
                                {
                                    p[contP].uR = i;
                                    p[contP].uC = colonne;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Find --> " + new string(cercata));
                                    Console.ResetColor();
                                    //stampiamo i valori delle parole
                                    //Console.WriteLine("Valori parola {0} \nR -> {1}\nC -> {2}\nM -> {3}", new string(cercata), p[contP].R, p[contP].C, p[contP].M);
                                    //Console.WriteLine("Valori parola {0} \nUR -> {1}\nUC -> {2}\nM -> {3}", new string(cercata), p[contP].uR, p[contP].uC, p[contP].M);
                                    //Console.WriteLine("Valori parola {0} \nUR -> {1}\nUC -> {2}\nM -> {3}", new string(cercata), p[contP].uR, p[contP].uC, p[contP].M);
                                    contP++;
                                }
                                cont++;
                            }
                        }
                    }
                }
                //contatore per la condizione del ciclo e delle parola da cercare
                k++;
            }
        }

        static void RicercaNelleOblique(char[,] puzzle, string[] parole, ValoriParole[] p)
        {
            int cont, k = 0;
            int rC, cC;

            char[] ricercate;
            char[] cercata = new char[11];

            while (k < parole.Length)
            {
                ricercate = parole[k].ToCharArray();
                for (int righe = 0; righe < 10; righe++)
                {
                    for (int colonne = 0; colonne < 10; colonne++)
                    {
                        //quando trovo la prima lettera della parola cercata controllo quelle dopo
                        if (puzzle[righe, colonne] == ricercate[0])
                        {
                            p[contP].R = righe;
                            p[contP].C = colonne;
                            p[contP].M = "diagonale";

                            //inizializzo le variabili per quando entro dentro il ciclo
                            rC = righe;
                            cC = colonne;
                            cont = 0;
                            Array.Clear(cercata, 0, cercata.Length);

                            for (int r = righe; r > 0; r--)
                            {
                                if (rC == 10 || cC == 10)
                                    break;
                                else
                                {
                                    cercata[cont] = puzzle[rC, cC];

                                    if (string.Compare(new string(cercata), new string(ricercate)) == 0)
                                    {
                                        p[contP].uC = cC;
                                        p[contP].uR = rC;
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Find --> " + new string(cercata));
                                        Console.ResetColor();
                                        contP++;
                                    }
                                    cont++;
                                }

                                rC--;
                                cC++;
                            }
                        }
                    }
                }
                k++;
            }
        }

        static void RicercaNelleObliqueRevers(char[,] puzzle, string[] parole, ValoriParole[] p)
        {
            int cont, k = 0;
            int rC, cC;

            char[] ricercate = new char[parole.Length];
            char[] cercata = new char[11];

            while (k < parole.Length)
            {
                Array.Clear(ricercate, 0, ricercate.Length);
                ricercate = parole[k].ToCharArray();

                for (int righe = 0; righe < 10; righe++)
                {
                    for (int colonne = 0; colonne < 10; colonne++)
                    {
                        //quando trovo la prima lettera della parola cercata controllo quelle dopo
                        if (puzzle[righe, colonne] == ricercate[0])
                        {
                            p[contP].R = righe;
                            p[contP].C = colonne;
                            p[contP].M = "diagonaleInversa";

                            //inizializzo le variabili per quando entro dentro il ciclo
                            rC = righe;
                            cC = colonne;
                            cont = 0;
                            Array.Clear(cercata, 0, cercata.Length);

                            for (int c = colonne; c < 10 - colonne; c++)
                            {
                                if (rC == 10 || cC == 10)
                                    break;
                                else
                                {
                                    cercata[cont] = puzzle[rC, cC];

                                    if (string.Compare(new string(cercata), new string(ricercate)) == 0)
                                    {
                                        p[contP].uC = cC;
                                        p[contP].uR = rC;
                                        p[contP].R = righe;
                                        p[contP].C = colonne;

                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Find --> " + new string(cercata));
                                        Console.ResetColor();

                                        contP++;
                                    }

                                    cont++;
                                }

                                rC++;
                                cC++;
                            }
                        }
                    }
                }
                k++;
            }
        }
        
        static void RicercaNelleRigheRevers(char[,] puzzle, string[] parole, ValoriParole[] p)
        {
            int cont, k = 0;
            char[] ricercate = new char[parole.Length];
            char[] cercata = new char[11];

            while (k < parole.Length)
            {
                Array.Clear(ricercate, 0, ricercate.Length);
                ricercate = parole[k].ToCharArray();
                //Array.Reverse(ricercate);
                for (int righe = 0; righe < 10; righe++)
                {
                    for (int colonne = 9; colonne >= 0; colonne--)
                    {
                        //quando trovo la prima lettera della parola cercata controllo quelle dopo
                        if (puzzle[righe, colonne] == ricercate[0])
                        {
                            //metto i valori della posizione della prima lettera nella struttura
                            p[contP].uR = righe;
                            p[contP].uC = colonne;
                            p[contP].M = "rigaInversa";

                            //imposto contatore e vettore a 0 così quando entro nel ciclo sono liberi
                            cont = 0;
                            Array.Clear(cercata, 0, cercata.Length);
                            for (int i = colonne; i > 0; i--)
                            {
                                cercata[cont] = puzzle[righe, i];

                                //se la stringa della parola trovata è uguale alla stringa cercata allora ritorno in ris
                                if (string.Compare(new string(cercata), new string(ricercate)) == 0)
                                {
                                    p[contP].C = i;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Find --> " + new string(cercata));
                                    Console.ResetColor();
                                    //stampo i valori delle parole
                                    //Console.WriteLine("Valori parola {0} \nUR -> {1}\nUC -> {2}\nM -> {3}", new string(cercata), p[contP].uR, p[contP].uC, p[contP].M);
                                    //Console.WriteLine("Valori parola {0} \nR -> {1}\nC -> {2}\nM -> {3}", new string(cercata), p[contP].R, p[contP].C, p[contP].M);
                                    contP++;
                                }
                                cont++;
                            }
                        }
                    }
                }
                //contatore per la condizione del ciclo e delle parola da cercare
                k++;
            }
        }
        
        static void RicercaNelleColonneRevers(char[,] puzzle, string[] parole, ValoriParole[] p)
        {
            int cont, k = 0;
            char[] ricercate;
            char[] cercata = new char[11];

            while (k < parole.Length)
            {
                ricercate = parole[k].ToCharArray();
                for (int righe = 9; righe >= 0; righe--)
                {
                    for (int colonne = 0; colonne < 10; colonne++)
                    {
                        //quando trovo la prima lettera della parola cercata controllo quelle dopo
                        if (puzzle[righe, colonne] == ricercate[0])
                        {
                            p[contP].C = colonne;
                            p[contP].R = righe;
                            p[contP].M = "verticaleInversa";

                            //imposto indice e vettore a 0 così quando entro nel ciclo sono liberi
                            cont = 0;
                            Array.Clear(cercata, 0, cercata.Length);
                            for (int i = righe; i >= 0; i--)
                            {
                                cercata[cont] = puzzle[i, colonne];
                                //se la stringa della parola trovata è uguale alla stringa cercata allora ritorno in ris
                                if (string.Compare(new string(cercata), new string(ricercate)) == 0)
                                {
                                    p[contP].uR = i;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Find --> " + new string(cercata));
                                    Console.ResetColor();

                                    //stampiamo i valori delle parole
                                    //Console.WriteLine("Valori parola {0} \nR -> {1}\nC -> {2}\nM -> {3}", new string(cercata), p[contP].R, p[contP].C, p[contP].M);
                                    //Console.WriteLine("Valori parola {0} \nUR -> {1}\nUC -> {2}\nM -> {3}", new string(cercata), p[contP].uR, p[contP].uC, p[contP].M);
                                    contP++;
                                }
                                cont++;
                            }
                        }
                    }
                }
                //contatore per la condizione del ciclo e delle parola da cercare
                k++;
            }
        }
    }
}