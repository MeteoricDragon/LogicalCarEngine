using LogicalEngine.EngineParts;
using LogicalEngine.Engines;

namespace LogicalEngine
{
    public interface IOutput
    {
        void ChangeCycleReport(CombustionStrokeCycles cycle);
        void ConnectedPartsFooter(UnitContainer part);
        void ConnectedPartsHeader(UnitContainer part);
        void DrainReport(UnitContainer p, int drainAmount);
        void EngineCycleCount(int cycles);
        void FillReport(UnitContainer p, int fillAmount);
        void Legend();
        void TakeFromReservoirFailReport(string name);
        void TransferHeader(UnitContainer partSender, CarPart partReceiver);
    }
}