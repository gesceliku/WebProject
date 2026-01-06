$username=$("#username")
$password=$("#password")
$role=$("#role")

function signIn(event){
    const username=$username.val();
    const password=$password.val();
    const role=$role.val();

    if(role==Client){
    $.ajax({
        url: `http://localhost:5288/api/Clients/GetClientByUsername?clientUn=${username}`,
        method: "GET",
        success: function (response) {
          if(username==response.username && password==response.password){
            localStorage.setItem("username", username);
            window.location.href="./dentalclinic-index.html"
          }
        },
        error: function (error) {
          console.error(error);
        },
      });
    }
    else if(role==Doctor){
        $.ajax({
            url: `http://localhost:5288/api/Doctors/GetDoctorByUsername?doctorUn=${username}`,
            method: "GET",
            success: function (response) {
              if(username==response.username && password==response.password){
                localStorage.setItem("username", username);
                window.location.href="./doctor.html"
              }
            },
            error: function (error) {
              console.error(error);
            },
          });
    }
    else {
        $.ajax({
            url: `http://localhost:5288/api/Admins/GetAdminByUsername?adminUn=${username}`,
            method: "GET",
            success: function (response) {
              if(username==response.username && password==response.password){
                window.location.href="./admin-page.html"
              }
            },
            error: function (error) {
              console.error(error);
            },
          });
    }

}