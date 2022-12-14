namespace Keyshift.Core.Interfaces
{
    public interface IKeyframe<TKeytype>
    {
        TKeytype CurrentValue { get; set; }
    }
}
