//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VIVLIO
{
    using System;
    using System.Collections.Generic;
    
    public partial class OFFER
    {
        public int OFFERID { get; set; }
        public int MATIEREID { get; set; }
        public int NIVEAUID { get; set; }
        public int USERID { get; set; }
        public decimal PRICE { get; set; }
        public System.DateTime CREATIONDATE { get; set; }
        public Nullable<System.DateTime> POSTEDDATE { get; set; }
        public string STATUS { get; set; }
        public byte[] PHOTO { get; set; }
        public string AUTHOR_COMPANYNAME { get; set; }
        public string CONDITION { get; set; }
        public string DESCRIPTION { get; set; }
        public string NAME { get; set; }
        public string PICTURE { get; set; }
    
        public virtual MATIERE MATIERE { get; set; }
        public virtual NIVEAU NIVEAU { get; set; }
        public virtual Users Users { get; set; }
    }
}
