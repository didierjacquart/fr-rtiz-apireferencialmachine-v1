using System;

namespace BDM.ReferencialMachine.Core.Model
{ 
    public class TimePeriod
    { 
        /// <summary>
        /// Date de fermeture à la souscription en AN (sans effet en remplacement)
        /// </summary>
        /// <value>Date de fermeture à la souscription en AN (sans effet en remplacement)</value>
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// Date d&#x27;ouverture à la souscription en AN (sans effet en remplacement)
        /// </summary>
        /// <value>Date d&#x27;ouverture à la souscription en AN (sans effet en remplacement)</value>
        public DateTime? StartDateTime { get; set; }

    }
}
