namespace BDM.ReferencialMachine.Core.Model
{ 
    public class EditionClause
    {
        /// <summary>
        /// Code de la clause éditique
        /// </summary>
        /// <value>Code de la clause</value>
        public string Code { get; set; }

        /// <summary>
        /// Libellé de la clause éditique
        /// </summary>
        /// <value>Libellé de la clause</value>
        public string Label { get; set; }

        /// <summary>
        /// Type de la clause éditique
        /// </summary>
        /// <value>Type de la clause</value>
        public string Type { get; set; }

        /// <summary>
        /// Description de la clause éditique
        /// </summary>
        /// <value>Description de la clause</value>
        public string Description { get; set; }

        /// <summary>
        /// Type de Clause : EDITION (par default), DECLARATION
        /// </summary>
        /// <value>Type de clause</value>
        public string ClauseType { get; set; }
    }
}
