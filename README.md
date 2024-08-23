Este juego esta basado en la famosa franquicia de videojuegos 'Pokemon'. 
Para el funcionamiento de este juego se utiliza como recurso la api pokeApi
que contiene una base de datos con la informacion de los distintos pokemones,
items, juegos y generaciones producto de esta saga.  Sin embargo, este juego
solo hara uso de los pokemones pertenecientes a la primera generacion.

El juego consiste en 10 rondas donde deberas enfrentarte a diferentes jefes
para pasar a la siguiente ronda.
Se generaran 10 pokemones aleatorios traidos de la api, o del archivo predeterminado
de personajes en caso de conexion fallida
Al final de cada ronda tu pokemon subira de nivel y podras elegir entre
diferentes recompensas en caso de ganar.
Si fuiste vencido en un combate con un jefe, este subira de nivel y pelearas
con el proximo pokemon disponible.
El juego finalizara en el caso que te quedes sin pokemones para pelear o cuando
hayas derrotado al ultimo de los jefes.

LINK de la API: https://pokeapi.co/api/v2/