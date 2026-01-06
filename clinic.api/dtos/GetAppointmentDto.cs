namespace DentalClinic.Dtos.Responses
{
    public class GetAppointmentDto
    {
        public string ClientUsername { get; set; }

        public string DoctorUsername { get; set; }
        public string ClientName { get; set; }

        public string DoctorName { get; set; }
        public string date { get; set; }
        public string time {  get; set; }
        public string clinic {  get; set; }

    }
}
