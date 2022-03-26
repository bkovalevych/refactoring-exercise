using Domain.Models;

namespace Domain.Rules.FeeRules.Rules
{
    public class FeeInSpecialTimeRule : AbstractFeeRule
    {
        private List<FeeSlot> FeeSlots { get;} = new List<FeeSlot>()
        {
            new FeeSlot()
            {
                TimeFrom = new TimeOnly(6, 0),
                TimeTo = new TimeOnly(6, 29),
                Cost = 8
            },
            new FeeSlot()
            {
                TimeFrom = new TimeOnly(6, 30),
                TimeTo = new TimeOnly(6, 59),
                Cost = 13
            },
            new FeeSlot()
            {
                TimeFrom = new TimeOnly(7, 0),
                TimeTo = new TimeOnly(7, 59),
                Cost = 18
            },
            new FeeSlot()
            {
                TimeFrom = new TimeOnly(8, 0),
                TimeTo = new TimeOnly(8, 29),
                Cost = 13
            },
            new FeeSlot()
            {
                TimeFrom = new TimeOnly(8, 30),
                TimeTo = new TimeOnly(14, 59),
                Cost = 8
            },
            new FeeSlot()
            {
                TimeFrom = new TimeOnly(15, 0),
                TimeTo = new TimeOnly(15, 29),
                Cost = 13
            },
            new FeeSlot()
            {
                TimeFrom = new TimeOnly(15, 30),
                TimeTo = new TimeOnly(16, 59),
                Cost = 18
            },
            new FeeSlot()
            {
                TimeFrom = new TimeOnly(17, 0),
                TimeTo = new TimeOnly(17, 59),
                Cost = 13
            },
            new FeeSlot()
            {
                TimeFrom = new TimeOnly(18, 0),
                TimeTo = new TimeOnly(18, 29),
                Cost = 8
            },
            new FeeSlot()
            {
                TimeFrom = new TimeOnly(18, 30),
                TimeTo = new TimeOnly(5, 59),
                Cost = 0
            }
        };

        public override int Evaluate(DateTime inputParameter)
        {
            var time = TimeOnly.FromDateTime(inputParameter);
            return FeeSlots.FirstOrDefault(x => x.TimeFrom <= time && time <= x.TimeTo)?.Cost ?? 0;
        }
    }
}
