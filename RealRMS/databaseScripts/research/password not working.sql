/*

Create
-password = yuckfou
-salt = vbalkl/KA9q1x9xpC4hH+P6uKWc=

GenerateHash 
-salt = vbalkl/KA9q1x9xpC4hH+P6uKWc=
-input = yuckfou
-bytes [35]
		[0]	121	byte
		[1]	117	byte
		[2]	99	byte
		[3]	107	byte
		[4]	102	byte
		[5]	111	byte
		[6]	117	byte
		[7]	118	byte
		[8]	98	byte
		[9]	97	byte
		[10]	108	byte
		[11]	107	byte
		[12]	108	byte
		[13]	47	byte
		[14]	75	byte
		[15]	65	byte
		[16]	57	byte
		[17]	113	byte
		[18]	49	byte
		[19]	120	byte
		[20]	57	byte
		[21]	120	byte
		[22]	112	byte
		[23]	67	byte
		[24]	52	byte
		[25]	104	byte
		[26]	72	byte
		[27]	43	byte
		[28]	80	byte
		[29]	54	byte
		[30]	117	byte
		[31]	75	byte
		[32]	87	byte
		[33]	99	byte
		[34]	61	byte

Create
- hash = $I%�Xl��,����B��|�&��O	�S!sa
         $I%�Xl��,����B��|�&��O	�S!sa
		 $I%?Xl??,????B??|?&??O	?S!sa
		 $I%?Xl??,????B??|?&??O	?S!sa
DB
- password = $I%?Xl??,????B??|?&??O	?S!sa
- salt = vbalkl/KA9q1x9xpC4hH

UPDATE [USER] SET salt = 'vbalkl/KA9q1x9xpC4hH+P6uKWc=' where id = 4
select * from [user]


DECLARE 
@gibberish VARCHAR(500) = '$I%�Xl��,����B��|�&��O	�S!sa'

select @gibberish;
*/