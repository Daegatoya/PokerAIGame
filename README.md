# PokerAIGame 🃏

## Qu'est-ce que c'est?

C'est un projet (une grande première pour moi) utilisant l'intelligence artificille (pas en self-learning) pour simuler une partie de poker (ou du moins, plus une manche...). Les réactions de l'intelligence artificielle diffère selon les cartes dans sa main et sur le jeu. 

## Fonctionnement

### Au début de la partie, l'IA commence le jeu. 

Si l'IA commence avec une paire,
```cs
if (ses_Numeros[0] == ses_Numeros[2])
  {
      mise = possibilité_Adverse[random.Next(1, 4)];
          Console.WriteLine($"{nom} commence et mise {mise}");
      }
  }
  ```
  
  Si l'IA commence avec un début de flush, 
  ```cs
  else if (ses_Numeros[1] == ses_Numeros[3] && ((ses_Numeros[0] == (int.Parse(ses_Numeros[2]) + 1).ToString()) || 
  (ses_Numeros[0] == (int.Parse(ses_Numeros[2]) - 1).ToString())))
            {
                mise = possibilité_Adverse[random.Next(1, 3)];
                Console.WriteLine($"{nom} commence et mise {mise}");
            }
```
Sinon,
```cs
 else
    {
      mise = 10;
      Console.WriteLine($"{nom} commence et mise {mise}");
    }
```

### Ensuite viens les tours

Vous commencer avec une action,
```cs
Console.WriteLine("\n\n\t1. Check/Follow\t  2. Fold\t  3. Raise");
            Console.Write("\n\tQue souhaitez-vous faire : ");
            ma_mise = Action(ref isCheck1, mise, ma_mise, vos_Cartes, ref wallet, joueur.ses_Cartes, cartes_Communes);
            Console.WriteLine();
```

Et les tours s'enchaînent avec les différentes fonctions ```Tour#();```

### Pour finir, la fin

Une fonction ```Fin();``` en void s'execute donc, c'est la fin de la partie,
```cs
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
```

## Résumé

Ce programme est donc ma première essaie avec l'intelligence artificielle. N'ayant pas trop d'experience dans cette sphère, j'ai préféré m'en tenir qu'à l'intelligence programmé plutôt qu'au "Self Learning". Quelques erreurs peuvent être notifiées dans le code, il n'est pas à 100% optimisé, en effet. J'espère qu'il vous plaîra quand même, car il représente pour moi une grande étape dans mon parcour. Merci!

---

**With love, Daegatoya** ❤️
         
<p align="center">

![My Discord](https://discord-readme-badge.vercel.app/api?id=852663698803130389)
</p>

                                                    Make it 1 or 0.
