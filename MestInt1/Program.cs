namespace MestInt1
{
    // Állapottér Rep = (A, k, C, O, PRE, POST)
    // A: állapotok halmaza
    // A {a | ÁllapotE(a) } -> igaz hamis értéket ad vissza a predikátum
    // k: kezdő állapot (mindenki a jobb parton van)
    // C: célállapotok halmaza -> C = {a | CélÁllapotE(a) }
    // O: operátorok halmaz
    // o eleme O, akkor o: A -> A 
    // ha: o(a) = b, akkor a,b eleme A
    // PRE: előfeltételek halmaza
    // POST: utófeltételek halmaza

    class SzKProblema
    {
        int kb;
        int kj;
        int szb;
        int szj;
        string cs; // melyik parton van (int jobb lenne legjobb megoldás, lehetne amúgy még bool is!)

        public SzKProblema()
        {
            this.kb = 0;
            this.szb = 0;
            this.kj = 3;
            this.szj = 3;
            this.cs = "jobb";
        }


        public bool AllapotE()
        {
            return kb >= 0 && kb <= 3 &&
                szb >= 0 && szb <= 3 &&
                kj >= 0 && kj <= 3 &&
                szj >= 0 && szj <= 3 &&
                (cs == "bal" || cs==  "jobb") && 
                kb + kj == 3 && szb + szj == 3 &&
                (szb >= kb || szb == 0) && 
                (szj >= kj || szj == 0);
        }    
        
        public bool CelAllapotE()
        {
            return AllapotE() && szb == 3 && kb == 3;
        }

        public int OperatorokSzama()
        {
            // operátor ami lényeges részletet változtat meg
            // amikor átküldöm a csónakot -> atvisz(0,1): átviszik arról az oldaról ahol a csónak van, 0 szerzetest és egy kannibált
            // atvisz(1,0) 
            // atvisz(1,1)
            // atvisz(2,0)
            // atvisz(0,2)
            return 5;
        }

        public bool SzuperOperator(int i)
        {
            // mindig egy rohadt nagy switch :)
            switch(i)
            {
                case 0: return atvisz(0, 1);
                case 1: return atvisz(1, 1);
                case 2: return atvisz(1, 0);
                case 3: return atvisz(0, 2);
                case 4: return atvisz(2, 0);
                default: return false;
            }
        }

        private bool atvisz(int sz, int k)
        {
            // ez egy alap operátor
            // minden alap operátor felépítése ugyanaz
            // előfeltétel vizsgálat
            // állapotátmenet
            // utófeltétel vizsgálat
            // Elmélet: Def(alkalmazható op):
            // Azt mondjuk, hogy kis o operátor alkalmazható 
            //  a állapotra, akkor és csak akkor, ha <=> pre_o(a) igaz és
            //                                           post_o(a) igaz, ahol
            //                                           b = o(a)


            // előfeltétel vizsgálat
            // aranyszabály: ugyanaz a param listája, mint az metódusnak!
            if(!preAtvisz(sz,k)) return false;

            // állapotátmenet
            // TODO
            if (cs == "bal")
            {
                //  bal -> jobb
                szb -= sz;
                szj += sz;
                kb -= k;
                kj += k;
                cs = "jobb";
            } else
            {
                // jobb -> bal
                szb += sz;
                szj -= sz;
                kb += k;
                kj -= k;
                cs = "bal";
            }

            // utófeltétel vizsgálat
            if (AllapotE()) return true;
            else return false;
           // return AllapotE(); // egyszerűbben! 
        }

        private bool preAtvisz(int sz, int k)
        {
            // mikor tudok átvinni sz db szerzetest és 
            // k db kannibált
            // ha az azdott oldalon van leblább
            // sz db szerzeets és k db kannibál
            if (cs == "bal") 
                return szb >= sz && kb >= k;
            else 
                return szj >= sz && kj >= k;
        }
    }
}
