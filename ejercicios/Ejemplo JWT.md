Ejemplo JWT



Cliente ----> /api/artistas
             <---- Lista de Artistas en JSON ---- Servidor

Cliente ---> /api/artistas?yosoy=pepito
            Servidor Chequea que exista pepito en la bd
            Pepito existe en la bd
            <----- Lista de Artistas en JSON ---- Servidor

Cliente ---> /api/artistas?yosoy=pepito@gmail.com&password=1234
            Servidor Chequea que exista pepito en la bd
            Pepito existe en la bd
            Pepito tiene el password correcto
            <----- Lista de Artistas en JSON ---- Servidor

Cliente ---> /api/artistas?yosoy=pepito@gmail.com&password=asfd12312FDSAjkjlkj
            Servidor Chequea que exista pepito en la bd
            Pepito existe en la bd
            Pepito tiene el password correcto
            <----- Lista de Artistas en JSON ---- Servidor

Cliente ---> /api/artistas?yosoy=ASLDKFJALSKFJALKSJFLKSDJ1231231DSFL

Cliente ---> /api/usuarios/login?email=pepito@gmail.com&password=1234
            Servidor Chequea que exista pepito en la bd
            Pepito existe en la bd
            Pepito tiene el password correcto
            <----- Token JWT ---- Servidor
Cliente Guarda el Token JWT
Cliente ---> /api/artistas?token=yeyhehjsad
