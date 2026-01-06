
const $clinicName=$("#clinicSelect");
const $doctorsList = $("#doctorsList");
const $clientName=localStorage.getItem("username")

function loadDoctors(){
    const clinic = document.getElementById("clinicSelect").value; 
    $.ajax({
        url: `http://localhost:5201/api/Doctors/GetDoctorByClinic?clinic=${clinic}`,
        method: "GET",
        success: function (response) {
          displayDoctors(response);
        },
        error: function (error) {
          console.error(error);
        },
      });
}

function displayDoctors(doctors) {
    $doctorsList.empty();
    doctors.forEach((doctor) => {
      const $row = $("<tr></tr>");
  
      $row.html(`
        <td>${doctor.name}</td>
        <td>
          <button class="btn btn-warning btn-sm book-btn" data-id="${doctor.username}">Book now</button>
        </td>
      `);
  
      $airlinesList.append($row);
    });
  }
  
  function submitBooking(doctorname) {
    const clientname = JSON.parse(localStorage.getItem('username'))
    const doctor = doctorname;
    const time = document.getElementById("appointmentTime").value;
    const date = document.getElementById("appointmentDate").value;
    $.ajax({
      url: "http://localhost:5288/api/Appointments/CreateAppointment",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify({ name: clientName, doctorname : doctor, time : time, date : date  
       }),
      success: function (response) {

      },
      error: function (error) {
        console.error(error);
      },
    });
    $appointmentsList.on("click", ".book-btn", function () {
      document.getElementById("bookingFormContainer").hidden=false;
      const doctorname= $(this).data("username");
      
      submitBooking(doctorname);
    });

    
}


