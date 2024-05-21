namespace BDM.ReferencialMachine.Core.Model
{
    public class DeductibleInterval
    {
       public bool? MaximumInclude { get; set; }
       public int? MaximumValue { get; set; }
       public bool? MinimumInclude { get; set; }
       public int? MinimumValue { get; set; }
       public string? Unit { get; set; }
    }
}
