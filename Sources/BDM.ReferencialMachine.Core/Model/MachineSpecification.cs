using System.Collections.Generic;

namespace BDM.ReferencialMachine.Core.Model
{
    /// <summary>
    /// Informations de la machine venant des référentiels
    /// </summary>
    public class MachineSpecification 
    {
        /// <summary>
        /// Code de la machine
        /// </summary>
        /// <value>Code de la machine</value>
        public string Code { get; set; }

        /// <summary>
        /// Nom de la machine
        /// </summary>
        /// <value>Nom de la machine</value>
        public string Name { get; set; }

        /// <summary>
        /// Libellé de la machine
        /// </summary>
        /// <value>Libellé de la machine</value>
        public string Label { get; set; }

        /// <summary>
        /// Description de la machine
        /// </summary>
        /// <value>Description de la machine</value>
        public string Description { get; set; }

        /// <summary>
        /// Identifiant des sous-familles d&#x27;activitées rattachées à cette machine
        /// </summary>
        /// <value>Identifiant des sous-familles d&#x27;activitées rattachées à cette machine</value>
        public ICollection<Family> Families { get; set; }

        /// <summary>
        /// Mots-clé de recherche
        /// </summary>
        /// <value>Mots-clé de recherche</value>
        public string[] Keywords { get; set; }

        /// <summary>
        /// Rang d&#x27;affichage de la machine (plus le score est grand, plus elle est à afficher en premier)
        /// </summary>
        /// <value>Rang d&#x27;affichage de la machine (plus le score est grand, plus elle est à afficher en premier)</value>
        public string Score { get; set; }

        /// <summary>
        /// Gets or Sets SubscriptionPeriod
        /// </summary>
        public TimePeriod SubscriptionPeriod { get; set; }

        /// <summary>
        /// Est-ce que la souscription de cette machine est délégable ? (Si non, alors DAP)
        /// </summary>
        /// <value>Est-ce que la souscription de cette machine est délégable ? (Si non, alors DAP)</value>
        public bool? IsDelegated { get; set; }

        /// <summary>
        /// Est-ce qu&#x27;il s&#x27;agit d&#x27;une machine non référencée avec un code générique ?
        /// </summary>
        /// <value>Est-ce qu&#x27;il s&#x27;agit d&#x27;une machine non référencée avec un code générique ?</value>
        public bool? IsUnreferenced { get; set; }

        /// <summary>
        /// Est-ce que la machine est exclue de la souscription ?
        /// </summary>
        /// <value>Est-ce que la machine est exclue de la souscription ?</value>
        public bool? IsExcluded { get; set; }

        /// <summary>
        /// Est-ce que la machine est exclue de la souscription pour BDM+ ?
        /// </summary>
        /// <value>Est-ce que la machine est exclue de la souscription pour BDM+ ?</value>
        public bool? IsOutOfMachineInsurance { get; set; }

        /// <summary>
        /// Est-ce que la machine est mobile ?
        /// </summary>
        /// <value>Est-ce que la machine est mobile ?</value>
        public bool? IsTransportable { get; set; }

        /// <summary>
        /// Age limite autorisée pour la souscription de la machine
        /// </summary>
        /// <value>Age limite autorisée pour la souscription de la machine</value>
        public int? AgeLimitAllowed { get; set; }

        /// <summary>
        /// Est-ce que la garantie en tout lieux doit, peut ou ne peut pas etre souscrite pour cette machine ? 
        /// </summary>
        /// <value>Est-ce que la garantie en tout lieux doit, peut ou ne peut pas etre souscrite pour cette machine ?
        /// (POINTLESS,OPTIONAL, INCLUDED)</value>
        public string AllPlacesCovered { get; set; }

        /// <summary>
        /// Pourcentage d&#x27;extension de la garantie au parc maximum possible pour cette machine (en %)
        /// </summary>
        /// <value>Pourcentage d&#x27;extension de la garantie au parc maximum possible pour cette machine (en %)</value>
        public int? ExtendedFleetCoverageAllowedPercentage { get; set; }

        /// <summary>
        /// Produit pour lequel la machine est rattachée (inventaire, parc, photovoltaique, atouts location, drone ...)
        /// </summary>
        /// <value>Produit pour lequel la machine est rattachée (inventaire, parc, photovoltaique, drone)</value>
        public string Product { get; set; }

        /// <summary>
        /// Taux machine pris en compte dans le calcul des garanties dommages
        /// </summary>
        /// <value>Taux machine pris en compte dans le calcul des garanties dommages</value>
        public decimal? MachineRate { get; set; }

        /// <summary>
        /// Coefficient appliqué dans le calcul des garanties
        /// </summary>
        /// <value>Coefficient appliqué dans le calcul des garanties</value>
        public ICollection<PricingRate> PricingRates { get; set; }

        /// <summary>
        /// Clauses automatiques des machines
        /// </summary>
        /// <value>Clauses automatiques des machines</value>
        public ICollection<EditionClause> EditionClauses { get; set; }

        public ICollection<RiskPrecision> RiskPrecisions { get; set; }

        public Deductible AgreementDeductible { get; set; }

        public int? FinancialValuationDelegatedMax { get; set; }

        public bool? IsFireCoverageMandatory { get; set; }

        /// <summary>
        /// Montant unitaire maximum des machines louées par défaut
        /// </summary>
        public CurrencyAmount FinancialValuationMachinedMax { get; set; }

        /// <summary>
        /// Montant unitaire maximum des machines louées assurable en délégation
        /// </summary>
        public CurrencyAmount FinancialValuationMachineDelegatedMax { get; set; }

    }
}
