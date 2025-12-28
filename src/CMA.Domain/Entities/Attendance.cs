namespace CMA.Domain.Entities;

public class Attendance : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    
    public DateTime Date { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public AttendanceStatus Status { get; set; }
    public decimal? WorkHours { get; set; }
    public decimal? OvertimeHours { get; set; }
    public string? Notes { get; set; }
}

public enum AttendanceStatus
{
    Present,
    Absent,
    Leave,
    HalfDay,
    Holiday,
    WeekOff
}
