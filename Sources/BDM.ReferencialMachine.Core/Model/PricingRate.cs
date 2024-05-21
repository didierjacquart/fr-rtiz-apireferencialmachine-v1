namespace BDM.ReferencialMachine.Core.Model
{ 
    public class PricingRate 
    { 
        /// <summary>
        /// Code de la garantie pour laquelle un coefficient est à appliquer
        /// </summary>
        /// <value>Code de la garantie pour laquelle un coefficient est à appliquer</value>
        public string Code { get; set; }

        /// <summary>
        /// Coefficient appliqué dans le calcul de la garantie pour la machine
        /// </summary>
        /// <value>Coefficient appliqué dans le calcul de la garantie pour la machine</value>
        public decimal? Rate { get; set; }
    }
}
