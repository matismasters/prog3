Cliente ------> /api/artistas ---> Servidor
Respuesta JSON con todos los artistas <------- Servidor

Cliente ------> /api/artistas?yosoy=pepito ---> Servidor
if usuario es pepito:
Respuesta JSON con todos los artistas <------- Servidor
else:
Respuesta JSON con error 403 <------- Servidor

Cliente ------> /api/artistas?yosoy=pepito&password=1234 ---> Servidor
if usuario es pepito y password es 1234:
Respuesta JSON con todos los artistas <------- Servidor
else:
Respuesta JSON con error 403 <------- Servidor

Cliente ------> /api/artistas?api_key=1234 ---> Servidor
if api_key esta en la base de datos :
Respuesta JSON con todos los artistas <------- Servidor
else: no devuelvo
Respuesta JSON con error 403 <------- Servidor

Cliente ------> /api/usuarios/login?usuario=pepito&password=1234 ---> if existe un usuario pepito con password 1234:
Respuesta JSON con token JWT <------- Servidor
else:
Respuesta JSON con error 403 <------- Servidor

Cliente Guarda el Token JWT
Cliente ------> /api/artistas?token=yeyhehjsad ---> Servidor
if token JWT es válido:
Respuesta JSON con todos los artistas <------- Servidor
else:
Respuesta JSON con error 403 <------- Servidor
