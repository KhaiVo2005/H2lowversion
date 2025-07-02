using H2.Domain.Enums;

namespace H2.Application.Helpers
{
    public static class AlertEvaluator
    {
        public static AlertLevel Evaluate(double hydrogenPpm) =>
            hydrogenPpm switch
            {
                < 100 => AlertLevel.Normal,
                < 200 => AlertLevel.Warning,
                < 300 => AlertLevel.Danger,
                _ => AlertLevel.Critical
            };
    }
}
