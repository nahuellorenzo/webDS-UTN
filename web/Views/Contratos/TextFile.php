<?php

require_once('C:\Users\User\Desktop\AGUS\Trabajo Grupal\E5\Aplicación\webMolino\web\Data\Migrations\ApplicationDbContextModelSnapshot.cs');

$search_criteria = $_POST['search_criteria'];

//cadena de consulta
$query = "SELECT numero, fechaEmision, fechaLimite, precioTonelada, metodoDePago
FROM Contrato WHERE numero LIKE '%".search_criteria."%'";

$contratos = [];
$errores = ['data' => false];

$getContratos = $b->query($query);

if($getContratos->num_rows > 0){
	while($data = $getContratos->fetch_assoc()){
		$contratos[] = $data;
	}
	echo json_encode($contratos);
} else{
	echo json_encode($errores);
}
