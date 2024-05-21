using System.Collections.Generic;

namespace BDM.ReferencialMachine.Core.Model
{
    public class Family
    { 
        public string Code { get; set; }

        /// <summary>
        /// Nom de la famille
        /// </summary>
        /// <value>Nom de la famille</value>
        public string Name { get; set; }

        /// <summary>
        /// Liste des sous familles
        /// </summary>
        /// <value>Liste des sous familles</value>
        public ICollection<SubFamily> SubFamilies { get; set; }
    }
}
