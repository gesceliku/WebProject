

$appointmentsList=$('#appointmentsList')

function loadAppointments(){
    const client = JSON.parse(localStorage.getItem("username")); 
    $.ajax({
        url: `http://localhost:5288/api/Appointments/GetAppointmentByClient?client=${client}`,
        method: "GET",
        success: function (response) {
          displayAppointments(response);
        },
        error: function (error) {
          console.error(error);
        },
      });
}

function displayAppointments(appointments) {
    $appointmentsList.empty();
    appointments.forEach((appointment) => {
      const $row = $("<tr></tr>");
  
      $row.html(`
        <td>${appointment.Clients.Fullname}</td>
        <td>${appointment.Doctors.Name}</td>
        <td>${appointment.time}</td>
        <td>${appointment.date}</td>
        <td>${appointment.clinic}</td>
        <td>
          <button class="btn btn-warning btn-sm delete-btn" data-id="${appointment.id}">Cancel</button>
        </td>
      `);
  
      $appointmentsList.append($row);
    });
    function deleteAppointment(id) {
        $.ajax({
          url: `http://localhost:5288/api/Appointments/DeleteAppointmentById?airlineId=${id}`,
          method: "DELETE",
          success: function (response) {
            loadAppointments();
          },
          error: function (error) {
            console.error(error);
          },
        });
      }
      

    $airlinesList.on("click", ".delete-btn", function () {
        const id = $(this).data("id");
        deleteAppointment(id);
      });
  }