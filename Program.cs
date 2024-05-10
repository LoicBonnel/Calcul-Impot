using System;
using System.Collections.Generic;
using static System.Console;

namespace impot
{
    class Program
    {
        static void Main(string[] args)
        {
            int nbrAdulte, nbrEnfant, nbrSalaire;
            var listSalaireBrut = new List<double>();
            var listSalaireNet = new List<double>();
            double montantImpot, QuotientFamilliale, Revenu, Abattement, Imposition, totalpart = 0;

            //  foreach (var name in listSalaireBrut) ;

            nbrEnfant = NbrMenage("Veuillez saisir le nombre d'enfant(s) dans le ménage");
            nbrAdulte = NbrMenage("Veuillez saisir le nombre d'adulte(s) dans le ménage");

            nbrSalaire = NbrSalaire();

            listSalaireBrut = DetailSalaire(nbrSalaire);
            listSalaireNet = CalculSalaireNet(nbrSalaire, listSalaireBrut);

            Revenu = RI(listSalaireNet, nbrSalaire);
            montantImpot = CalculPart(nbrEnfant, nbrAdulte, ref totalpart); 
            Abattement = Calculabattement(montantImpot, Revenu);
            QuotientFamilliale = QF(totalpart, Revenu);
            Imposition = MontantImposition(QuotientFamilliale, Abattement);

            Console.WriteLine("Vous devez donc payer : " + Imposition + " euros d'impots.");
        }

        #region Collecte Salaire

        static string Saisir(string prmMessage)
        {
            Console.WriteLine(prmMessage);
            return Console.ReadLine();
        }/// <summary>
         /// Permet de stocker une saisie 
         /// </summary>
         /// <param name="prmMessage"></param>
         /// <returns></returns>

        static int SaisirEntier(string prmMessage)
        {
            string saisie;
            int reponse;
            do
            {
                saisie = Saisir(prmMessage);
            } while (!Int32.TryParse(saisie, out reponse));
            return reponse;
        }/// <summary>
         /// permet de controller les utilisateurs non intelligent et de les forcer à saisir un (chiffre)
         /// </summary>
         /// <param name="prmMessage"></param>
         /// <returns></returns>

        static double SaisirDouble(string prmMessage)
        {
            string saisie;
            double reponse;
            do
            {
                saisie = Saisir(prmMessage);
            } while (!Double.TryParse(saisie, out reponse));
            return reponse;
        }/// <summary>
         /// (controller les utilisateurs non intelligent) les forcer à saisir un (chiffre)
         /// </summary>
         /// <param name="prmMessage"></param>
         /// <returns></returns>

        static string OuiNon(string prmMessage)
        {
            string saisie;
            do
            {
                saisie = Saisir(prmMessage);
                saisie = saisie.ToLower();
            } while (saisie != "o" && saisie != "n");
            return saisie;
        }/// <summary>
         /// permet de valider les saisie en demendant à l'utilisateur
         /// </summary>
         /// <param name="prmMessage"></param>
         /// <returns></returns>

        static int NbrMenage(string PRMmessage)
        {
            int saisie;
            string reponse;
            do
            {
                saisie = SaisirEntier(PRMmessage);
                Console.WriteLine("Il y a donc " + saisie + " " + PRMmessage.Substring(28, 9));
                reponse = OuiNon("Est-ce bien ça (o) Oui / (n) Non");
            } while (reponse == "n");
            reponse = "";
            return saisie;
        }/// <summary>
         /// recolter le nombre de ménage
         /// </summary>
         /// <returns></returns>

        static int NbrSalaire()
        {
            int saisie;
            string reponse;
            do
            {
                saisie = SaisirEntier("Veuillez entrer le nombre de salaire dans le ménage.");

                Console.WriteLine("Vous avez bien " + saisie + " salaires dans le ménage.");
                reponse = OuiNon("Est-ce bien ça (o) Oui / (n) Non");
            } while (reponse == "n");
            reponse = "";
            return saisie;
        } /// <summary>
          /// recolter le nombre de Salaire
          /// </summary>
          /// <param name="prmNombresalaire"></param>
          /// <returns></returns>

        static System.Collections.Generic.List<double> DetailSalaire(int prmNombresalaire)
        {
            int SalaireMalEcrit;
            string reponse;
            double Salaire;
            var listSalaireBrut = new List<double>();

            for (int i = 1; i <= prmNombresalaire; i++)
            {
                Salaire = SaisirDouble("Veuillez saisir le salaire n° " + i);
                listSalaireBrut.Add(Salaire);
            }

            Console.WriteLine("Vos salaires sont donc :");

            for (int G = 0; G < prmNombresalaire; G++)
            {
                Console.Write("n° " + (G + 1) + " : ");
                Console.WriteLine(listSalaireBrut[G]);
            }
            reponse = OuiNon("Est-ce bien ça (o) Oui / (n) Non");
            while (reponse == "n")
            {
                do
                {
                    SalaireMalEcrit = SaisirEntier("Quelle salaire n'est pas bien saisie, (1, 2, 3 ...) ? ");
                } while (SalaireMalEcrit > prmNombresalaire || SalaireMalEcrit <= 0);


                double NouvelleReponse = SaisirDouble("Veuillez resaisir le salaire n° " + SalaireMalEcrit);
                SalaireMalEcrit = SalaireMalEcrit - 1;

                listSalaireBrut[SalaireMalEcrit] = NouvelleReponse;

                Console.WriteLine("Vos salaires sont donc :");
                for (int G = 0; G < prmNombresalaire; G++)
                {
                    Console.Write("n° " + (G + 1) + " : ");
                    Console.WriteLine(listSalaireBrut[G]);
                }
                reponse = "";
                reponse = OuiNon("Est-ce bien ça (o) Oui / (n) Non");
            }


            reponse = "";
            return (listSalaireBrut);
        }/// <summary>
         /// recolter les salaire tout en controllant les erreurs des utilisateurs
         /// </summary>
         /// <param name="prmNombresalaire"></param>
         /// <param name="ListSalaireBrut"></param>
         /// <returns></returns>

        #endregion Collecte Salaire
        #region Calcule Imposition
        static System.Collections.Generic.List<double> CalculSalaireNet(int prmNombresalaire, System.Collections.Generic.List<double> ListSalaireBrut)
        {
            var listSalaireNet = new List<double>();

            for (int i = 0; i < prmNombresalaire; i++)
            {
                if (ListSalaireBrut[i] < 4410 && ListSalaireBrut[i] >= 441)
                {
                    listSalaireNet.Add(ListSalaireBrut[i] - 441);
                }
                else
                {
                    if (ListSalaireBrut[i] >= 4410 && ListSalaireBrut[i] <= 126270)
                    {
                        listSalaireNet.Add(ListSalaireBrut[i] * 0.90);
                    }
                    else
                    {
                        listSalaireNet.Add(ListSalaireBrut[i] - 12627);
                    }
                }
            }
            return (listSalaireNet);
        }/// <summary>
         /// Calculer le salaire Net 
         /// </summary>
         /// <param name="ListSalaireNet"></param>
         /// <param name="prmNombresalaire"></param>
         /// <returns></returns>

        static double RI(System.Collections.Generic.List<double> ListSalaireNet, int prmNombresalaire)
        {
            double total = 0;
            for (int i = 0; i < prmNombresalaire; i++)
            {
                total += ListSalaireNet[i];
            }
            return total;
        } /// <summary>
          /// Calcule du RI
          /// </summary>
          /// <param name="nbrAdulte"></param>
          /// <param name="nbrEnfant"></param>
          /// <param name="totalpart"></param>
          /// <returns></returns>

        static double CalculPart(int nbrAdulte, int nbrEnfant, ref double totalpart)
        {
            double partEnfant, montantImpot, total;
            int partAdulte;
            total = nbrAdulte + nbrEnfant;
            partAdulte = nbrAdulte;
            if (nbrAdulte >= 2)
            {
                if (nbrEnfant >= 3)
                {
                    partEnfant = nbrEnfant * 1 - 1; //3 enfants -> 3 - 1 -> 2 * 0.5 + 1  
                }
                else
                {
                    partEnfant = nbrEnfant * 0.5;
                }
            }
            else
            {
                partEnfant = nbrEnfant;
            }
            totalpart = partEnfant + partAdulte;
            montantImpot = 21036 * partAdulte + 3737 * partEnfant; // 3737 part demi parts
            return montantImpot;
        }/// <summary>
         /// Calcul des parts des ménages
         /// </summary>
         /// <param name="totalpart"></param>
         /// <param name="RI"></param>
         /// <returns></returns>

        static double QF(double totalpart, double RI)
        {
            double QF;
            QF = RI / totalpart;
            return QF;
        } /// <summary>
          /// Calcule du QF 
          /// </summary>
          /// <param name="montantimpo"></param>
          /// <param name="RI"></param>
          /// <returns></returns>

        static double Calculabattement(double montantimpo, double RI)
        {
            double reduction = 0;
            if (RI < montantimpo)
            {
                reduction = 0.80;
            }
            else
            {
                reduction = 1;
            }
            return reduction;
        }
        /// <summary>
        /// Calculer l'abattement 
        /// </summary>
        /// <param name="QF"></param>
        /// <param name="abattement"></param>
        /// <param name="ri"></param>
        /// <returns></returns>

        static double MontantImposition(double QF, double abattement)  

        {
            double impo = 0;
         
            if (QF >= 157806)
            {
                impo += (QF - 157806)  * 0.45 + (QF - 73370) * 0.41 + (QF - 25660) * 0.30 + (QF - 10065) * 0.11;
            }
            else
            {
                if (QF >= 73370)
                {
                    impo += (QF - 73370) * 0.41 + (QF - 25660) * 0.30 + (QF - 10065) * 0.11;
                }
                else
                {
                    if (QF >= 25660)
                    {
                        impo += (QF - 25660) * 0.30 + (QF - 10065) * 0.11;
                    }
                    else
                    {
                        if (QF >= 10065)
                        {
                            impo += (QF - 10065) * abattement * 0.11;
                        }
                    }
                }
            }
            return impo;
        }
    }
}
#endregion Calcule Imposition

// Programme de Loïc Bonnel et de Enzo Delattre --> janvier