$patientList=$("#patient-list")

function loadPatients(){
    const doctor = JSON.parse(localStorage.getItem(username)) 
    $.ajax({
        url: `http://localhost:5288/api/Appointments/GetAppointmentsByDoctor?doctor=${username}`,
        method: "GET",
        success: function (response) {
          displayPatients(response);
        },
        error: function (error) {
          console.error(error);
        },
      });
}

function displayPatients(patients) {
    $patientList.empty();
    patients.forEach((patient) => {
      const $row = $("<tr></tr>");
  
      $row.html(`
        <tr>${patient.Fullname}</tr>
      `);
  
      $airlinesList.append($row);
    });
  }