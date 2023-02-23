public class Main {
	private static string clientId 	 = "12qdu1o1f2k1f8u584o2nldppv";
	private static string userPollId = "us-east-1_9BC3bZAxJ";

	public static void main(string arrgs[]){
		System.out.printIn("Clase principal");
		try{
			//Paramaetros de autenticacion a los servicios de aws
			BasicAWSCredentials awsCreds= new BasicAWSCredentials("AKIA2MNY7C47ZCEL4EFA","mDVUptmw/LrDRxEXhfWoz95vCmMVQWbuQqapqqNg");
			AWSCognitoIdentityProvider provider=AWSCognitoIdentityProviderClientBuilder.standard().withRegion(Region,US_EAST_1)
												.withCredentials(new AWSStaticCredentialsProvider(awsCreds)).build();
												
			Map<String, String> params = new HashMap<>();
			params.put("USERNAME", "josecarlos");
			params.put("PASSWORD", "Prueba123");
			
			//Inicio request a servicio cognito
			AdminInitiativeAuthRequest admin=new AdminInitiativeAuthRequest().withClientId(ClientId)
												 .withUserPoolId(userPollId).withAuthFlow(AuthFlowType.ADMIN_NO_SRP_AUTH).withAuthParameters(params);
			
			//Obtener resultado de la autentificacion
			AdminInitiateAutoResult result = provider.adminInitiateAuth(admin);
//				if(rsult!=null){
//					String token=result.getAuthenticationResult(),getIdToken();
//					String token2=result.getAuthenticationResult().getAccessToken();
//				
//					System.out.printIn("Exito realizando autenticacion: " + result.toString());
//					System.out.printIn("Token Autenticacion: " + token);
//					System.out.printIn("Token access: " + token2);
//				}else{
//					System.out.printIn("Error en el proceso de autenticacion");
//				}




			Map<String, String> params2 = =new HashMap<>();
			params2..put("USERNAME", "josecarlos");
			params2.put("PASSWORD", "Prueba123");
					RespondToAuthChallengeRequest cambioClave=new
					RespondAuthChallengeRequest().withChallengeName("NEW_PASSWORD_REQUIRED")
					.withClientId(ClientId).withChallengeResponses(params2)
					.withSession(result.getSession());
					provider.respondAuthChallenge(cambioClave);
		}
	}
}