namespace XML_Exporter
{
    class CopyTypes
    {
        static int DATA_COPY; // 0 = obican copy, svaki red je druga vrednost
                              // 1 = concatenate copy, u svakom redu je ista vrednost a vrednost je concat svih mogucih vrednosti
                              // 2 = copy koji se trazi u DocInternals prva iteracija za table name
                              // 3 = copy koji uzima vrednost polja, ali trazi u prevodu (CSV fajl) koji je naveden i kolonu koja sadrzi tu vrednost
    }
}
