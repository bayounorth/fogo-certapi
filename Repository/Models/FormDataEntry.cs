using System.Threading.Tasks;

namespace FogoCertApi.Repository.Models
{
    public class FormDataEntry : BaseModel
    {
        public int ReportFormId { get; set; }
        public bool OkNo { get; set; }
        public string Signature { get; set; }
        public string PrintName { get; set; }
        public string VesselName { get; set; }
        public string Vessel { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public bool PortStarboard { get; set; }
        public string TripPoint { get; set; }
        public string TestPoint { get; set; }
        public int? MechanicsId { get; set; }
        public string MechanicsName { get; set; }
        public string Comments { get; set; }
        public string EngineType { get; set; }
        public string HighCoolTip { get; set; }
        public string HighCoolTest { get; set; }
        public string LowOilTrip { get; set; }
        public string LowOilTest { get; set; }
        public string ListOfUnitsTested { get; set; }
        public string ListOfDeficiencies { get; set; }
        public string ReportedTo { get; set; }
        public string PersonnelName { get; set; }
        public bool PortOkNo { get; set; }
        public bool StarboardOkNo { get; set; }
        public bool PortFaceOkNo { get; set; }
        public bool StarboardFaceOkNo { get; set; }
        public bool MooringLinesOkNo { get; set; }
        public string OnBoard { get; set; }
        public string Receiving { get; set; }
        public string FinaInGal { get; set; }
        public byte[] PicOfTransfer { get; set; }
        public string CheckList { get; set; }
        public string OfficialNumber { get; set; }
        public string GrossTonage { get; set; }
        public string CallSign { get; set; }
        public string PortOfRegistry { get; set; }
        public string VesselAddress { get; set; }
        public string ListOfItemsInspected { get; set; }
        public string ABCDEReport { get; set; }
        public string FieldList { get; set; }
        public string Site { get; set; }
        public string Topics { get; set; }
        public string Attendance { get; set; }
        public string Employee { get; set; }
        public string EmployeeSignature { get; set; }
        public string Conductor { get; set; }
        public string ListOfAttendees { get; set; }
        public string MechanicsDate { get; set; }
        public string CustomerDate { get; set; }
        public string EstimatedInches { get; set; }
        public string ShoresideTankLocation { get; set; }
        public string VesselHullYNA { get; set; }
        public string PropellersRuddersYNA { get; set; }
        public string SeaChestsYNA { get; set; }
        public string OtherServiceAreasYNA { get; set; }
        public string AnodesReplacedYNA { get; set; }
        public string RudderBearingsInspectedYNA { get; set; }
        public string HullCoatingAppliedYNA { get; set; }
        public string HullGaugingPerformedYNA { get; set; }
        public bool CustodianStatementYN { get; set; }
        public string CustodianRep { get; set; }
        public string CustodianTitle { get; set; }
        public string CustodianDate { get; set; }
        public string TowingInspectionDateTimeInspection { get; set; }
        public string TowingInspectionInspectorName { get; set; }
        public int TowingInspectionInspectorId { get; set; }
        public bool TowingInspectionPortWinch { get; set; }
        public bool TowingInspectionStarboardWinch { get; set; }
        public bool TowingInspectionPortFaceWire { get; set; }
        public bool TowingInspectionStarboardFaceWire { get; set; }
        public bool TowingInspectionMooringLines { get; set; }
        public string UA { get; set; }

    }
}
