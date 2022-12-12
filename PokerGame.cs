namespace PokerGame
{
    using System;
    public static class MainClass
    {

        public struct Joueur
        {
            public string nom;
            public string[] ses_Cartes;
        }

        public static void Main()
        {
            string[] logos = { "♠", "♥", "♦", "♣" };
            string[] cartes = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            string[] vos_Cartes = new string[2];
            string[] vos_Numeros = {};
            string[] ses_Numeros = {}; 
            Joueur joueur = new Joueur();
            joueur.ses_Cartes = new string[2];
            int wallet = 1000;
            int[] possibilité_Adverse = { 10, 50, 100, 500, 1000};
            int mise = 0;
            int ma_mise = 0;
            Random random = new Random();
            bool isCheck1 = false;
            bool isCheck2 = false;
            List<string> cartes_Tirées = new List<string>();
            string[] cartes_Communes = new string[3];
            string[] numéros_Communs;
            string[] numéros_Communs2;
            string[] numéros_Communs3;


            Console.Write("Quel nom souhaitez-vous donner à votre adversaire : ");
            joueur.nom = Console.ReadLine();
            joueur.ses_Cartes = Tirage(logos, cartes, 2, ref cartes_Tirées);
            ses_Numeros = cartes_Check(joueur.ses_Cartes);
            Console.Clear();

            Console.WriteLine("Vos cartes\tVotre argent");
            vos_Cartes = Tirage(logos, cartes, 2, ref cartes_Tirées);
            vos_Numeros = cartes_Check(vos_Cartes);
            foreach (string carte in vos_Cartes)
            {
                Console.Write(carte + " ");
            }

            Console.Write($"\t\t{wallet}");
            Console.WriteLine();
            Console.WriteLine();
            mise = Debut(joueur.nom, mise, possibilité_Adverse, random, ses_Numeros);

            Console.WriteLine("\n\n\t1. Check/Follow\t  2. Fold\t  3. Raise");
            Console.Write("\n\tQue souhaitez-vous faire : ");
            ma_mise = Action(ref isCheck1, mise, ma_mise, vos_Cartes, ref wallet, joueur.ses_Cartes, cartes_Communes);
            Console.WriteLine();

            do
            {
                mise = Tour(ref isCheck2, joueur.nom, mise, possibilité_Adverse, random, ses_Numeros, ma_mise, joueur.ses_Cartes, vos_Cartes, cartes_Communes);
                Console.WriteLine("\n\n\t1. Check/Follow\t  2. Fold\t  3. Raise");
                Console.Write("\n\tQue souhaitez-vous faire : ");
                ma_mise = Action(ref isCheck1, mise, ma_mise, vos_Cartes, ref wallet, joueur.ses_Cartes, cartes_Communes);
                Console.WriteLine();
            } while (isCheck1 == false || isCheck2 == false);

            mise = 0;
            ma_mise = 0;
            isCheck1 = false;
            isCheck2 = false;
            numéros_Communs = Check1(ref cartes_Communes, logos, cartes, ref cartes_Tirées);

            do
            {
                Console.WriteLine("\n\n\t1. Check/Follow\t  2. Fold\t  3. Raise");
                Console.Write("\n\tQue souhaitez-vous faire : ");
                ma_mise = Action(ref isCheck1, mise, ma_mise, vos_Cartes, ref wallet, joueur.ses_Cartes, cartes_Communes);
                Console.WriteLine();
                mise = Tour2(ref isCheck2, numéros_Communs, joueur.nom, ses_Numeros, mise, ma_mise, possibilité_Adverse, random, joueur.ses_Cartes, vos_Cartes, cartes_Communes);
            } while (isCheck1 == false || isCheck2 == false);

            mise = 0;
            ma_mise = 0;
            isCheck1 = false;
            isCheck2 = false;
            numéros_Communs2 = Check2(ref cartes_Communes, numéros_Communs, logos, cartes, ref cartes_Tirées);

            do
            {
                Console.WriteLine("\n\n\t1. Check/Follow\t  2. Fold\t  3. Raise");
                Console.Write("\n\tQue souhaitez-vous faire : ");
                ma_mise = Action(ref isCheck1, mise, ma_mise, vos_Cartes, ref wallet, joueur.ses_Cartes, cartes_Communes);
                Console.WriteLine();
                mise = Tour3(ref isCheck2, numéros_Communs2, joueur.nom, ses_Numeros, mise, ma_mise, possibilité_Adverse, random, joueur.ses_Cartes, vos_Cartes, cartes_Communes);
            } while (isCheck1 == false || isCheck2 == false);

            mise = 0;
            ma_mise = 0;
            isCheck1 = false;
            isCheck2 = false;
            numéros_Communs3 = Check3(ref cartes_Communes, numéros_Communs, logos, cartes, ref cartes_Tirées);

            do
            {
                Console.WriteLine("\n\n\t1. Check/Follow\t  2. Fold\t  3. Raise");
                Console.Write("\n\tQue souhaitez-vous faire : ");
                ma_mise = Action(ref isCheck1, mise, ma_mise, vos_Cartes, ref wallet, joueur.ses_Cartes, cartes_Communes);
                Console.WriteLine();
                mise = Tour4(ref isCheck2, numéros_Communs3, joueur.nom, ses_Numeros, mise, ma_mise, possibilité_Adverse, random, joueur.ses_Cartes, vos_Cartes, cartes_Communes);
            } while (isCheck1 == false || isCheck2 == false);

            Fin(vos_Cartes, joueur.ses_Cartes, cartes_Communes);
        }

        public static string[] Tirage(string[] logos, string[] cartes, int nb, ref List<string> cartes_Tirées)
        {
            string[] les_Cartes = new string[nb];
            Random random = new Random();
            string carte;
            

            for (int i = 0; i < nb; i++)
            {
                do
                {
                    carte = $"{cartes[random.Next(1, cartes.Length)]}{logos[random.Next(1, logos.Length)]}";
                } while (cartes_Tirées.Contains(carte));
                cartes_Tirées.Add(carte);
            les_Cartes[i] = carte;
            }

            return les_Cartes;
        }

        public static int Debut(string nom, int mise, int[] possibilité_Adverse, Random random, string[] ses_Numeros)
        {
            if (ses_Numeros[0] == ses_Numeros[2])
            {
                mise = possibilité_Adverse[random.Next(1, 4)];
                Console.WriteLine($"{nom} commence et mise {mise}");
            }
            else if (ses_Numeros[1] == ses_Numeros[3] && ((ses_Numeros[0] == (int.Parse(ses_Numeros[2]) + 1).ToString()) || (ses_Numeros[0] == (int.Parse(ses_Numeros[2]) - 1).ToString())))
            {
                mise = possibilité_Adverse[random.Next(1, 3)];
                Console.WriteLine($"{nom} commence et mise {mise}");
            }
            else
            {
                mise = 10;
                Console.WriteLine($"{nom} commence et mise {mise}");
            }
            return mise;
        }

        public static int Action(ref bool isCheck, int mise, int ma_mise, string[] vos_Cartes, ref int wallet, string[] ses_Cartes, string[] cartes_Communes)
        {
            int output;
            int? new_Mise;
            int? choix;

            do
            {
                choix = int.TryParse(Console.ReadLine(), out output) ? output : null;
                switch (choix)
                {
                    case null:
                        Console.Write("\n\nChoix inexistant. Réessayer : ");
                        break;

                    case 1:
                        if (mise != ma_mise)
                        {
                            if (mise > wallet) Fin(vos_Cartes, ses_Cartes, cartes_Communes);
                            else
                            {
                                ma_mise = mise;
                                wallet -= ma_mise;
                                Console.Clear();
                                Console.WriteLine("Vos cartes\tVotre argent");
                                foreach (string carte in vos_Cartes)
                                {
                                    Console.Write(carte + " ");
                                }

                                Console.Write($"\t\t{wallet}");
                                Console.WriteLine();
                                Console.WriteLine();
                                Console.WriteLine($"Vous suivez et misez {mise}");
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Vos cartes\tVotre argent");
                            foreach (string carte in vos_Cartes)
                            {
                                Console.Write(carte + " ");
                            }

                            Console.Write($"\t\t{wallet}");
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine($"Vous checkez");

                            isCheck = true;
                        }
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("\nVous vous couchez. Fin de la manche.");
                        Fin(vos_Cartes, ses_Cartes, cartes_Communes);
                        break;

                    case 3:
                        Console.Write("\nDe combien souhaitez-vous augmenter la mise : ");
                        do
                        {
                            new_Mise = int.TryParse(Console.ReadLine(), out output) ? output : null;
                            switch (new_Mise)
                            {
                                case null:
                                    Console.Write("\nMontant invalide. Réessayer : ");
                                    break;

                                default:
                                        ma_mise = (int)new_Mise + mise;
                                        wallet -= ma_mise;
                                    break;
                            }
                        } while (new_Mise == null) ;

                        Console.Clear();
                        Console.WriteLine("Vos cartes\tVotre argent");
                        foreach (string carte in vos_Cartes)
                        {
                            Console.Write(carte + " ");
                        }

                        Console.Write($"\t\t{wallet}");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine($"Vous montez la mise de {new_Mise} et suivez avec {mise}");
                        break;

                        default:
                        Console.Write("\nChoix inexistant. Réessayer : ");
                        choix = null;
                        break;
                }
            } while (choix == null);

            return ma_mise;
    }

        public static string[] Check1(ref string[] cartes_Communes, string[] logos, string[] cartes, ref List<string> cartes_Tirées)
        {
            cartes_Communes = new string[3];
            string[] les_Numéros;
            
            cartes_Communes = Tirage(logos, cartes, cartes_Communes.Length, ref cartes_Tirées);
            les_Numéros = cartes_Check(cartes_Communes);

            Console.WriteLine("\nLes cartes communes :");

            foreach (string carte in cartes_Communes)
            {
                Console.Write(carte + " ");
            }

            return les_Numéros;
        }

        public static string[] Check2(ref string[] cartes_Communes, string[] numéros_Communs, string[] logos, string[] cartes, ref List<string> cartes_Tirées)
        {
            string[] nouvelle_Carte = new string[1];
            string[] les_Numéros = new string[2];
            string[] nouveaux_Numéros = new string[numéros_Communs.Length + les_Numéros.Length];

            nouvelle_Carte = Tirage(logos, cartes, nouvelle_Carte.Length, ref cartes_Tirées);
            string[] nouvelles_Cartes = new List<string>()
                .Concat(cartes_Communes)
                .Concat(nouvelle_Carte)
                .ToArray();
            cartes_Communes = nouvelles_Cartes;
            les_Numéros = cartes_Check(nouvelle_Carte);

            Console.WriteLine("\nLes cartes communes :");
            foreach (string carte in cartes_Communes)
            {
                Console.Write(carte + " ");
            }

            numéros_Communs.CopyTo(nouveaux_Numéros, 0);
            les_Numéros.CopyTo(nouveaux_Numéros, numéros_Communs.Length);
            return nouveaux_Numéros;
        }

        public static string[] Check3(ref string[] cartes_Communes, string[] numéros_Communs, string[] logos, string[] cartes, ref List<string> cartes_Tirées)
        {
            string[] nouvelle_Carte = new string[1];
            string[] les_Numéros = new string[2];
            string[] nouveaux_Numéros = new string[numéros_Communs.Length + les_Numéros.Length];

            nouvelle_Carte = Tirage(logos, cartes, nouvelle_Carte.Length, ref cartes_Tirées);
            string[] nouvelles_Cartes = new List<string>()
                .Concat(cartes_Communes)
                .Concat(nouvelle_Carte)
                .ToArray();
            cartes_Communes = nouvelles_Cartes;
            les_Numéros = cartes_Check(nouvelle_Carte);

            Console.WriteLine("\nLes cartes communes :");
            foreach (string carte in cartes_Communes)
            {
                Console.Write(carte + " ");
            }

            numéros_Communs.CopyTo(nouveaux_Numéros, 0);
            les_Numéros.CopyTo(nouveaux_Numéros, numéros_Communs.Length);
            return nouveaux_Numéros;
        }

        public static int Tour(ref bool isCheck, string nom, int mise, int[] possibilité_Adverse, Random random, string[] ses_Numeros, int ma_mise, string[] ses_Cartes, string[] vos_Cartes, string[] cartes_Communes)
        {
            int result;

            if (mise == ma_mise)
            {
                if (ses_Numeros[0] == ses_Numeros[2])
                {
                    result = random.Next(1, 6);
                    switch (result)
                    {
                        case 1:
                            mise = possibilité_Adverse[random.Next(1, 3)];
                            Console.WriteLine($"{nom} monte la mise de {mise}");
                            break;
                        default:
                            Console.WriteLine($"{nom} check");
                            isCheck = true;
                            break;
                    }
                }
                else
                {
                    result = random.Next(1, 4);
                    switch (result)
                    {
                        case 1:
                            mise = possibilité_Adverse[random.Next(1, 3)];
                            Console.WriteLine($"{nom} monte la mise de {mise}");
                            break;
                        default:
                            Console.WriteLine($"{nom} check");
                            isCheck = true;
                            break;
                    }
                }
            }
            else
            {
                if (ses_Numeros[0] == ses_Numeros[2])
                {
                    mise = ma_mise;
                    Console.WriteLine($"{nom} suit la mise de {mise}");
                }
                else
                {
                    result = random.Next(1, 11);
                    switch (result)
                    {
                        case 1:
                            Console.WriteLine($"{nom} se couche");
                            Fin(vos_Cartes, ses_Cartes, cartes_Communes);
                            break;
                        default:
                            mise = ma_mise;
                            Console.WriteLine($"{nom} suit la mise de {mise}");
                            break;
                    }
                }
            }
            return mise;
        }


        public static int Tour2(ref bool isCheck, string[] les_Numéros, string nom, string[] ses_Numeros, int mise, int ma_mise, int[] possibilité_Adverse, Random random, string[] ses_Cartes, string[] vos_Cartes, string[] cartes_Communes)
        {
            int result;

            if (mise == ma_mise)
            {
                if (ses_Numeros[0] == ses_Numeros[2])
                {
                        result = random.Next(1, 6);
                        switch (result)
                        {
                            case 1:
                            case 2:
                                Console.WriteLine($"{nom} check");
                                isCheck = true;
                                break;
                            default:
                                mise = possibilité_Adverse[random.Next(1, 4)];
                                Console.WriteLine($"{nom} monte la mise de {mise}");
                                break;
                        }
                }
                else if (ses_Numeros[2] == les_Numéros[0] || ses_Numeros[2] == les_Numéros[2] || ses_Numeros[2] == les_Numéros[4] ||
  ses_Numeros[0] == les_Numéros[0] || ses_Numeros[0] == les_Numéros[2] || ses_Numeros[0] == les_Numéros[4])
                {
                    result = random.Next(0, 6);
                    switch (result)
                    {
                        case 1:
                        case 2:
                            Console.WriteLine($"{nom} check");
                            isCheck = true;
                            break;
                        default:
                            mise = possibilité_Adverse[random.Next(1, 5)];
                            Console.WriteLine($"{nom} monte la mise de {mise}");
                            break;
                    }
                }
                else if (ses_Numeros[1] == ses_Numeros[3] && ((ses_Numeros[0] == (int.Parse(ses_Numeros[2]) + 1).ToString()) || (ses_Numeros[0] == (int.Parse(ses_Numeros[2]) - 1).ToString())) && (ses_Numeros[3] == les_Numéros[1] || ses_Numeros[3] == les_Numéros[3]))
                {
                    result = random.Next(1, 11);
                    switch (result)
                    {
                        case 1:
                        case 2:
                        case 3:
                            Console.WriteLine($"{nom} check");
                            isCheck = true;
                            break;
                        default:
                            mise = possibilité_Adverse[random.Next(1, 3)];
                            Console.WriteLine($"{nom} monte la mise de {mise}");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"{nom} check");
                    isCheck = true;
                }
            }
            else
            {
                if (ses_Numeros[0] == ses_Numeros[2])
                {
                    for (int i = 0; i < les_Numéros.Length; i++)
                    {
                        if (i != 1 || i != 3 || i != 5)
                        {
                            if (ses_Numeros[0] == les_Numéros[i])
                            {
                                mise = ma_mise;
                                Console.WriteLine($"{nom} suit la mise de {mise}");
                            }
                        }
                        else
                        {
                            result = random.Next(1, 11);
                            switch (result)
                            {
                                case 1:
                                    Console.WriteLine($"{nom} se couche");
                                    Fin(vos_Cartes, ses_Cartes, cartes_Communes);
                                    break;
                                default:
                                    mise = ma_mise;
                                    Console.WriteLine($"{nom} suit la mise de {mise}");
                                    break;
                            }
                        }
                    }
                }
                else if (ses_Numeros[2] == les_Numéros[0] || ses_Numeros[2] == les_Numéros[2] || ses_Numeros[2] == les_Numéros[4] ||
                    ses_Numeros[0] == les_Numéros[0] || ses_Numeros[0] == les_Numéros[2] || ses_Numeros[0] == les_Numéros[4])
                {
                    result = random.Next(1, 11);
                    switch (result)
                    {
                        case 1:
                            Console.WriteLine($"{nom} se couche");
                            Fin(vos_Cartes, ses_Cartes, cartes_Communes);
                            break;
                        default:
                            mise = ma_mise;
                            Console.WriteLine($"{nom} suit la mise de {mise}");
                            break;
                    }
                }
                else if (ses_Numeros[1] == ses_Numeros[3] && ((ses_Numeros[0] == (int.Parse(ses_Numeros[2]) + 1).ToString()) || (ses_Numeros[0] == (int.Parse(ses_Numeros[2]) - 1).ToString())) && (ses_Numeros[3] == les_Numéros[1] || ses_Numeros[3] == les_Numéros[3]))
                {
                    result = random.Next(1, 11);
                    switch (result)
                    {
                        case 1:
                        case 2:
                            Console.WriteLine($"{nom} se couche");
                            Fin(vos_Cartes, ses_Cartes, cartes_Communes);
                            break;
                        default:
                            mise = ma_mise;
                            Console.WriteLine($"{nom} suit la mise de {mise}");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"{nom} se couche");
                    Fin(vos_Cartes, ses_Cartes, cartes_Communes);
                }
            }
            return mise;
        }

        public static int Tour3(ref bool isCheck, string[] les_Numéros, string nom, string[] ses_Numeros, int mise, int ma_mise, int[] possibilité_Adverse, Random random, string[] ses_Cartes, string[] vos_Cartes, string[] cartes_Communes)
        {
            int result;

            if (mise == ma_mise)
            {
                if (ses_Numeros[0] == ses_Numeros[2])
                {
                                result = random.Next(1, 6);
                                switch (result)
                                {
                                    case 1:
                                    case 2:
                                        Console.WriteLine($"{nom} check");
                                        isCheck = true;
                                        break;
                                    default:
                                        mise = possibilité_Adverse[random.Next(1, 4)];
                                        Console.WriteLine($"{nom} monte la mise de {mise}");
                                        break;
                    }
                }
                else if (ses_Numeros[1] == ses_Numeros[3] && ((ses_Numeros[0] == (int.Parse(ses_Numeros[2]) + 1).ToString()) || (ses_Numeros[0] == (int.Parse(ses_Numeros[2]) - 1).ToString())) && (ses_Numeros[3] == les_Numéros[1] || ses_Numeros[3] == les_Numéros[3] || ses_Numeros[3] == les_Numéros[5]))
                {
                    result = random.Next(1, 11);
                    switch (result)
                    {
                        case 1:
                        case 2:
                        case 3:
                            Console.WriteLine($"{nom} check");
                            isCheck = true;
                            break;
                        default:
                            mise = possibilité_Adverse[random.Next(1, 3)];
                            Console.WriteLine($"{nom} monte la mise de {mise}");
                            break;
                    }
                }
                else if (ses_Numeros[2] == les_Numéros[0] || ses_Numeros[2] == les_Numéros[2] || ses_Numeros[2] == les_Numéros[4] || ses_Numeros[2] == les_Numéros[6] ||
  ses_Numeros[0] == les_Numéros[0] || ses_Numeros[0] == les_Numéros[2] || ses_Numeros[0] == les_Numéros[4] || ses_Numeros[0] == les_Numéros[6])
                {
                    result = random.Next(0, 6);
                    switch (result)
                    {
                        case 1:
                        case 2:
                            Console.WriteLine($"{nom} check");
                            isCheck = true;
                            break;
                        default:
                            mise = possibilité_Adverse[random.Next(1, 5)];
                            Console.WriteLine($"{nom} monte la mise de {mise}");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"{nom} check");
                    isCheck = true;
                }
            }
            else
            {
                if (ses_Numeros[0] == ses_Numeros[2])
                {
                    for (int i = 0; i < les_Numéros.Length; i++)
                    {
                        if (i != 1 || i != 3 || i != 5 || i != 7)
                        {
                            if (ses_Numeros[0] == les_Numéros[i])
                            {
                                mise = ma_mise;
                                Console.WriteLine($"{nom} suit la mise de {mise}");
                            }
                        }
                        else
                        {
                            result = random.Next(1, 11);
                            switch (result)
                            {
                                case 1:
                                    Console.WriteLine($"{nom} se couche");
                                    Fin(vos_Cartes, ses_Cartes, cartes_Communes);
                                    break;
                                default:
                                    mise = ma_mise;
                                    Console.WriteLine($"{nom} suit la mise de {mise}");
                                    break;
                            }
                        }
                    }
                }
                else if (ses_Numeros[2] == les_Numéros[0] || ses_Numeros[2] == les_Numéros[2] || ses_Numeros[2] == les_Numéros[4] || ses_Numeros[2] == les_Numéros[6] ||
                    ses_Numeros[0] == les_Numéros[0] || ses_Numeros[0] == les_Numéros[2] || ses_Numeros[0] == les_Numéros[4] || ses_Numeros[0] == les_Numéros[6])
                {
                    result = random.Next(1, 11);
                    switch (result)
                    {
                        case 1:
                            Console.WriteLine($"{nom} se couche");
                            Fin(vos_Cartes, ses_Cartes, cartes_Communes);
                            break;
                        default:
                            mise = ma_mise;
                            Console.WriteLine($"{nom} suit la mise de {mise}");
                            break;
                    }
                }
                else if (ses_Numeros[1] == ses_Numeros[3] && ((ses_Numeros[0] == (int.Parse(ses_Numeros[2]) + 1).ToString()) || (ses_Numeros[0] == (int.Parse(ses_Numeros[2]) - 1).ToString())) && (ses_Numeros[3] == les_Numéros[1] || ses_Numeros[3] == les_Numéros[3] || ses_Numeros[3] == les_Numéros[5]))
                {
                    result = random.Next(1, 11);
                    switch (result)
                    {
                        case 1:
                        case 2:
                            Console.WriteLine($"{nom} se couche");
                            Fin(vos_Cartes, ses_Cartes, cartes_Communes);
                            break;
                        default:
                            mise = ma_mise;
                            Console.WriteLine($"{nom} suit la mise de {mise}");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"{nom} se couche");
                    Fin(vos_Cartes, ses_Cartes, cartes_Communes);
                }
            }
            return mise;
        }

        public static int Tour4(ref bool isCheck, string[] les_Numéros, string nom, string[] ses_Numeros, int mise, int ma_mise, int[] possibilité_Adverse, Random random, string[] ses_Cartes, string[] vos_Cartes, string[] cartes_Communes)
        {
            int result;

            if (mise == ma_mise)
            {
                if (ses_Numeros[0] == ses_Numeros[2])
                {
                                result = random.Next(0, 6);
                                switch (result)
                                {
                                    case 1:
                                    case 2:
                                        Console.WriteLine($"{nom} check");
                                        isCheck = true;
                                        break;
                                    default:
                                        mise = possibilité_Adverse[random.Next(1, 5)];
                                        Console.WriteLine($"{nom} monte la mise de {mise}");
                                        break;
                                }
                            }
                else if (ses_Numeros[1] == ses_Numeros[3] && ((ses_Numeros[0] == (int.Parse(ses_Numeros[2]) + 1).ToString()) || (ses_Numeros[0] == (int.Parse(ses_Numeros[2]) - 1).ToString())) && (ses_Numeros[3] == les_Numéros[1] || ses_Numeros[3] == les_Numéros[3] || ses_Numeros[3] == les_Numéros[5] || ses_Numeros[3] == les_Numéros[7]))
                {
                    result = random.Next(1, 11);
                    switch (result)
                    {
                        case 1:
                        case 2:
                        case 3:
                            Console.WriteLine($"{nom} check");
                            isCheck = true;
                            break;
                        default:
                            mise = possibilité_Adverse[random.Next(1, 3)];
                            Console.WriteLine($"{nom} monte la mise de {mise}");
                            break;
                    }
                }
                else if (ses_Numeros[2] == les_Numéros[0] || ses_Numeros[2] == les_Numéros[2] || ses_Numeros[2] == les_Numéros[4] || ses_Numeros[2] == les_Numéros[6] || ses_Numeros[0] == les_Numéros[8] ||
                  ses_Numeros[0] == les_Numéros[0] || ses_Numeros[0] == les_Numéros[2] || ses_Numeros[0] == les_Numéros[4] || ses_Numeros[0] == les_Numéros[6] || ses_Numeros[0] == les_Numéros[8])
                {
                    result = random.Next(0, 6);
                    switch (result)
                    {
                        case 1:
                        case 2:
                            Console.WriteLine($"{nom} check");
                            isCheck = true;
                            break;
                        default:
                            mise = possibilité_Adverse[random.Next(1, 5)];
                            Console.WriteLine($"{nom} monte la mise de {mise}");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"{nom} check");
                    isCheck = true;
                }
            }
            else
            {
                if (ses_Numeros[0] == ses_Numeros[2])
                {
                    for (int i = 0; i < les_Numéros.Length; i++)
                    {
                        if (i != 1 || i != 3 || i != 5 || i != 7 || i != 9)
                        {
                            if (ses_Numeros[0] == les_Numéros[i])
                            {
                                mise = ma_mise;
                                Console.WriteLine($"{nom} suit la mise de {mise}");
                            }
                        }
                        else
                        {
                            result = random.Next(1, 11);
                            switch (result)
                            {
                                case 1:
                                    Console.WriteLine($"{nom} se couche");
                                    Fin(vos_Cartes, ses_Cartes, cartes_Communes);
                                    break;
                                default:
                                    mise = ma_mise;
                                    Console.WriteLine($"{nom} suit la mise de {mise}");
                                    break;
                            }
                        }
                    }
                }
                else if (ses_Numeros[1] == ses_Numeros[3] && ((ses_Numeros[0] == (int.Parse(ses_Numeros[2]) + 1).ToString()) || (ses_Numeros[0] == (int.Parse(ses_Numeros[2]) - 1).ToString())) && (ses_Numeros[3] == les_Numéros[1] || ses_Numeros[3] == les_Numéros[3] || ses_Numeros[3] == les_Numéros[5] || ses_Numeros[3] == les_Numéros[7]))
                {
                    result = random.Next(1, 11);
                    switch (result)
                    {
                        case 1:
                        case 2:
                            Console.WriteLine($"{nom} se couche");
                            Fin(vos_Cartes, ses_Cartes, cartes_Communes);
                            break;
                        default:
                            mise = ma_mise;
                            Console.WriteLine($"{nom} suit la mise de {mise}");
                            break;
                    }
                }
                else if(ses_Numeros[2] == les_Numéros[0] || ses_Numeros[2] == les_Numéros[2] || ses_Numeros[2] == les_Numéros[4] || ses_Numeros[2] == les_Numéros[6] || ses_Numeros[0] == les_Numéros[8] ||
                    ses_Numeros[0] == les_Numéros[0] || ses_Numeros[0] == les_Numéros[2] || ses_Numeros[0] == les_Numéros[4] || ses_Numeros[0] == les_Numéros[8])
                {
                    result = random.Next(1, 11);
                    switch (result)
                    {
                        case 1:
                            Console.WriteLine($"{nom} se couche");
                            Fin(vos_Cartes, ses_Cartes, cartes_Communes);
                            break;
                        default:
                            mise = ma_mise;
                            Console.WriteLine($"{nom} suit la mise de {mise}");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"{nom} se couche");
                    Fin(vos_Cartes, ses_Cartes, cartes_Communes);
                }
            }
            return mise;
        }

        public static string[] cartes_Check(string[] cartes)
        {
            int nb = 0;
            int nb_Char = 0;
            string[] resultat;
            string[] characters = new string[100];

            if(cartes.Length == 2)
            {
                 resultat = new string[(cartes[0].ToString() + cartes[1].ToString()).Length];
            }
            else if (cartes.Length == 3)
            {
                resultat = new string[(cartes[0].ToString() + cartes[1].ToString() + cartes[2].ToString()).Length];
            }
            else
            {
                resultat = new string[(cartes[0].ToString()).Length];
            }

            for (int i = 0; i < cartes.Length; i++)
            {
                if (cartes[i] == "10" + "♠" || cartes[i] == "10" + "♥" || cartes[i] == "10" + "♦" || cartes[i] == "10" + "♣")
                {
                    foreach (char c in cartes[i])
                    {
                        characters[nb_Char] = c.ToString();
                        nb_Char++;
                    }

                    if (i == 1)
                    {
                        foreach (char c in cartes[i])
                        {
                            if (nb != i + 1 && nb != i + 2)
                            {
                                resultat[nb] = c.ToString();
                                nb++;
                            }
                            else
                            {
                                resultat[i + 1] = characters[0] + characters[1];
                                nb++;
                            }
                        }
                    }
                    else
                    {
                        foreach (char c in cartes[i])
                        {
                            if (nb != i && nb != i + 1)
                            {
                                resultat[nb] = c.ToString();
                                nb++;
                            }
                            else
                            {
                                resultat[i] = characters[0] + characters[1];
                                nb++;
                            }
                        }
                    }
                }
                else
                {
                    foreach (char c in cartes[i])
                    {
                        resultat[nb] = c.ToString();
                        nb++;
                    }
                }
            }
            for (int k = 0; k < resultat.Length; k++)
            {
                if (cartes.Length == 2)
                {
                    if (k == 0)
                    {
                        if (resultat[k] == "10")
                        {
                            resultat[k + 1] = resultat[k + 2];
                            resultat[k + 2] = resultat[k + 3];
                            resultat[k + 3] = resultat[k + 4];
                        }
                    }
                    if (k == 2)
                    {
                        if (resultat[k] == "10")
                        {
                            resultat[k + 1] = resultat[k + 2];
                        }
                    }
                }
                else if (cartes.Length == 3)
                {
                    if (k == 0)
                    {
                        if (resultat[k] == "10")
                        {
                            resultat[k + 1] = resultat[k + 2];
                            resultat[k + 2] = resultat[k + 3];
                            resultat[k + 3] = resultat[k + 4];
                            resultat[k + 4] = resultat[k + 5];
                            resultat[k + 5] = resultat[k + 6];
                        }
                    }
                    if (k == 2)
                    {
                        if (resultat[k] == "10")
                        {
                            resultat[k + 1] = resultat[k + 2];
                        }
                    }
                }
                else if (cartes.Length == 1)
                {
                    if (k == 0)
                    {
                        if (resultat[k] == "10")
                        {
                            resultat[k + 1] = resultat[k + 2];
                        }
                    }
                    if (k == 2)
                    {
                        if (resultat[k] == "10")
                        {
                            resultat[k + 1] = resultat[k + 2];
                        }
                    }
                }
            }
            return resultat;
        }

        public static void Fin(string[] vos_Cartes, string[] ses_Cartes, string[] cartes_Communes)
        {
            Console.Clear();
            Console.WriteLine("-----------------");
            Console.WriteLine("Résultats finaux");
            Console.WriteLine("-----------------");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Vos cartes\t\tCartes adverse\t\tCartes communes");

            foreach (string carte in vos_Cartes)
            {
                Console.Write(carte + " ");
            }
            Console.Write("\t\t\t");
            foreach (string carte in ses_Cartes)
            {
                Console.Write(carte + " ");
            }
            Console.Write("\t\t\t");
            foreach (string carte in cartes_Communes)
            {
                Console.Write(carte + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Merci d'avoir joué!");

            Environment.Exit(0x1);
        }
    }
}
