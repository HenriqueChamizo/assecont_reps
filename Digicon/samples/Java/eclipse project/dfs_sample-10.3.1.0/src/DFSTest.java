/* * * Digicon  - VCA Department ***
 * This is a simple sample, you can use with reference.
 * Version DFS: 10.3.1.0
 * Tank you for using Digicon solutions.
 * Enjoy.
 * 
 * Att 
 * Leonardo Secon Digicon-VCA/Developer
 * 20/04/2013 12:10:22
 */
import java.util.BitSet;
import java.util.Date;
import java.util.List;
import java.awt.image.ConvolveOp;
import java.nio.ByteBuffer;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import digiconframeworkserver.DigiconServer;
import digiconframeworkserver.control.DeviceManager;
import digiconframeworkserver.control.MessageManager;
import digiconframeworkserver.control.MessageManagerClock;
import digiconframeworkserver.control.MessageReceivedArgs;
import digiconframeworkserver.control.MessageReceivedArgsClock;
import digiconframeworkserver.control.MessageReceivedHandler;
import digiconframeworkserver.control.MessageReceivedHandlerClock;
import digiconframeworkserver.info.Session;
import digiconframeworkserver.logger.LogWriter;
import digiconframeworkserver.logger.WriteLogArgs;
import digiconframeworkserver.logger.WriteLogHandler;
import digiconframeworkserver.objects.commandobjects.Actuate;
import digiconframeworkserver.objects.commandobjects.ConfigFirmware;
import digiconframeworkserver.objects.commandobjects.DeviceConfig;
import digiconframeworkserver.objects.commandobjects.DeviceStatus;
import digiconframeworkserver.objects.commandobjects.DigitalOutStatus;
import digiconframeworkserver.objects.commandobjects.DisableEmergency;
import digiconframeworkserver.objects.commandobjects.EnableEmergency;
import digiconframeworkserver.objects.commandobjects.EventMessageScreen;
import digiconframeworkserver.objects.commandobjects.Function;
import digiconframeworkserver.objects.commandobjects.Input;
import digiconframeworkserver.objects.commandobjects.Reader;
import digiconframeworkserver.objects.commandobjects.UpdateDateTime;
import digiconframeworkserver.objects.messageobjects.DeviceConnectedMessageClock;
import digiconframeworkserver.objects.messageobjects.Message;
import digiconframeworkserver.objects.messageobjects.commandreturnobjects.CommandReturn;
import digiconframeworkserver.objects.messageobjects.eventobjects.EventConnChangeStatus;
import digiconframeworkserver.objects.messageobjects.eventobjects.EventMessage;
import digiconframeworkserver.objects.messageobjects.eventobjects.EventTemplateRegister;
import digiconframeworkserver.objects.messageobjects.requestobjects.AccessRequestCard;
import digiconframeworkserver.objects.messageobjects.requestobjects.AccessRequestPerson;
import digiconframeworkserver.objects.messageobjects.responseobjects.PendingData;
import digiconframeworkserver.objects.messageobjects.responseobjects.PendingStructure;
import digiconframeworkserver.objects.messageobjects.responseobjects.ResponseAccessValid;
import digiconframeworkserver.objects.messageobjects.responseobjects.ResponsePersonStructure;
import digiconframeworkserver.objects.messageobjects.responseobjects.ResponseTagStructure;
import digiconframeworkserver.objects.messageobjects.asyncobjects.*;
import digiconframeworkserver.objects.messageobjects.eventobjects.*;
import system.DateTime;
import system.Object;
import util.*;


public class DFSTest {


	public DFSTest() {

		LoadDFS load = new LoadDFS("./lib/cdfs-10.3.1.0.dll");

	}
	
	public void process() throws Exception {
		final DigiconServer DFS = new DigiconServer();
		
		
		MessageManagerClock.addMessageReceivedChangedClock(new MessageReceivedHandlerClock() {
			
			@Override
			public void Invoke(MessageReceivedArgsClock args) {
				// TODO Auto-generated method stub
				
				String msg = "";	      

	            if (args.getMsg() instanceof RequestPersonListResp) //
	            {
	            	DFS.Ack(args.getMsg().getDeviceID());
	                msg += "\nRetorno do comando RequestPeopleList " +
	               "\n\ngetDeviceID() " + ((RequestPersonListResp)args.getMsg()).getDeviceID() +
	                "\nTotPerson " + ((RequestPersonListResp)args.getMsg()).getTotperson() + "\n\n\n";

	                for (int i = 0; i < ((RequestPersonListResp)args.getMsg()).getTotperson(); i++)
	                {
	                    msg += i +
	                    	"\nNome " +((RequestPersonListResp)args.getMsg()).getPerson()[i].getName() + "\n" +
	                    "\nPersonID " + ((RequestPersonListResp)args.getMsg()).getPerson()[i].getPersonID() + "\n";
	                 
                    for (int x = 0; x < ((RequestPersonListResp)args.getMsg()).getPerson()[i].getCardClock().length; x++)
                    {
                        msg +=  "\nCardId " + ((RequestPersonListResp)args.getMsg()).getPerson()[i].getCardClock()[x].getCardID() + "\n";                        
                    }
	              
                    msg += "------------------------------------------------\n\n";
	                }  
	            }
	            else if (args.getMsg() instanceof RequestBackupLogResp) //
	            {
	            	DFS.Ack(args.getMsg().getDeviceID());
	            	 msg =  "\nRetorno do comando RequestBackupLogResp " +
	                "\ngetDeviceID() " + ((RequestBackupLogResp)args.getMsg()).getDeviceID() +
	                   "\nTot Event.....= " + ((RequestBackupLogResp)args.getMsg()).getTotEvent() + "\n";

	        

	                for (int i = 0, n = 1; i < (int)((RequestBackupLogResp)args.getMsg()).getTotEvent(); i++, n++)
	                {
	                    msg += "\nEvento nº " + n +
	                        "\nEvento  = " + EventClockTypeJava.valueOf(((RequestBackupLogResp)args.getMsg()).getEvents()[i].getEventID()) +
	                              "\nDate    = " + ((RequestBackupLogResp)args.getMsg()).getEvents()[i].getTimeEvent() + "\n";

		                    msg += "\n";
	                }

	            }
	            else if (args.getMsg() instanceof UpdateClockResp) //
	            {
	            	DFS.Ack(args.getMsg().getDeviceID());
	               msg = "\nRetorno do comando UpdateClock " +
	                "\ngetDeviceID() " + ((UpdateClockResp)args.getMsg()).getDeviceID() +
	                "\nStatus " + ((UpdateClockResp)args.getMsg()).getStatus() 
	                 ;

	            }
	            else if (args.getMsg() instanceof DeviceStatusClockResp) //
	            {
	            	DFS.Ack(args.getMsg().getDeviceID());
	               msg = "\nRetorno do comando RequestDeviceInfo " +
	                "\ngetDeviceID() " + ((DeviceStatusClockResp)args.getMsg()).getDeviceID() 
	                 ;

	            }
	            else if (args.getMsg() instanceof SetMifareMapResp) //
	            {
	            	DFS.Ack(args.getMsg().getDeviceID());
	              msg = "\nRetorno do comando SetMifareMap " +
	                "\ngetDeviceID() " + ((SetMifareMapResp)args.getMsg()).getDeviceID() 
	                 ;
	            }
	            else if (args.getMsg() instanceof ManagerCompanyResp) //
	            {
	            	DFS.Ack(args.getMsg().getDeviceID());
	                msg =
	               "\nRetorno do comando InsertUpdateCompany " +
	               "\ngetDeviceID() " + ((ManagerCompanyResp)args.getMsg()).getDeviceID();
	            }
	            else if (args.getMsg() instanceof InsertUpdatePersonResp)
	            {
	            	DFS.Ack(args.getMsg().getDeviceID());
	                msg = "Retorno do comando InsertUpdatePerson " +
	               "\ngetDeviceID() " + ((InsertUpdatePersonResp)args.getMsg()).getDeviceID() +
	               "\nPersonID " + ((InsertUpdatePersonResp)args.getMsg()).getPersonID() + 
	                "\nErrorCount " + ((InsertUpdatePersonResp)args.getMsg()).getErrorCount();
	                
	                for (int i = 0; i < ((InsertUpdatePersonResp)args.getMsg()).getErrorCount(); i++)
	                {
	                    msg += ((InsertUpdatePersonResp)args.getMsg()).getErrorCodeList()[i] + "\n";
	                }

	            }
	            else if (args.getMsg() instanceof InsertUpdateReadersResp) //
	            {
	            	DFS.Ack(args.getMsg().getDeviceID());
	                msg = "\nRetorno do comando InsertUpdateReaders " +
	               "\ngetDeviceID() " + ((InsertUpdateReadersResp)args.getMsg()).getDeviceID() +
	               "\nErrorCount " + ((InsertUpdateReadersResp)args.getMsg()).getErrorCount();
	                
	                for (int i = 0; i < ((InsertUpdateReadersResp)args.getMsg()).getErrorCount(); i++)
	                {
	                    msg += ((InsertUpdateReadersResp)args.getMsg()).getErrorCodeList()[i] + "\n";
	                }

	            }
	            else if (args.getMsg() instanceof DeletePersonResp) //
	            {
	            	DFS.Ack(args.getMsg().getDeviceID());
	                msg = "Retorno do comando DeletePersonResp " +
	               "\ngetDeviceID() " + ((DeletePersonResp)args.getMsg()).getDeviceID() +
	               "\nPersonID " + ((DeletePersonResp)args.getMsg()).getPersonID() +
	               "\nStatus " + ((DeletePersonResp)args.getMsg()).getStatus() ;

	            }
	            else if (args.getMsg() instanceof DeviceConnectedMessageClock) //
	            {
	            	msg = "Requisição de Conexão" +
							"\ngetDeviceID() : " + ((DeviceConnectedMessageClock)args.getMsg()).getDeviceID() +
							"\nDevice Type : " + ((DeviceConnectedMessageClock)args.getMsg()).getDeviceType() +
							 "\nFirmwareVersion : " + ((DeviceConnectedMessageClock)args.getMsg()).getFirmwareVersion();
	                                                                                 ;
	            }

	            else if (args.getMsg() instanceof RequestLogResp) //
	            {
	            	DFS.Ack(args.getMsg().getDeviceID());
	                msg = "\nEvento....= RequestLogResp " +
	                    "\ngetDeviceID().......= " + ((RequestLogResp)args.getMsg()).getDeviceID() + 
	                   "\nTot Event.....= " + ((RequestLogResp)args.getMsg()).getTotEvent() + "\n" ;


	                for (int i = 0, n = 1; i < (int)((RequestLogResp)args.getMsg()).getTotEvent(); i++, n++)
	                        {
	                            msg += "\nEvento nº " + n +
	                                "\nId Evento  = " + ((RequestLogResp)args.getMsg()).getEvents()[i].getEventID() +
	                                      "\nDate    = " + ((RequestLogResp)args.getMsg()).getEvents()[i].getTimeEvent() + "\n" ;

	                          msg += "\n";
	                        }
	            }
				System.out.println(msg);
			}
		});
		
		MessageManager.addMessageReceivedChanged(new MessageReceivedHandler() {
			@Override
			public void Invoke(MessageReceivedArgs args) {
				Util util = new Util();
				Message msg = args.getMsg();
				system.Enum en = (system.Enum) system.Enum.Parse(args.getMsg()
						.getMsgType().GetType(), args.getMsg().getMsgType()
						.toString());

				if ((int) en.GetHashCode() != MessageTypeJava.MSG_ACCESS_REQUEST_CARD.getValue() && 
						(int) en.GetHashCode() != MessageTypeJava.MSG_ACCESS_REQUEST_PERSON.getValue() &&
						(int) en.GetHashCode() != MessageTypeJava.MSG_TEMPLATE_REQUEST.getValue() &&
						(int) en.GetHashCode() != MessageTypeJava.MSG_DATA_REQUEST_CARD.getValue() &&
						(int) en.GetHashCode() != MessageTypeJava.MSG_DATA_REQUEST_PERSON.getValue() &&
						(int) en.GetHashCode() != MessageTypeJava.MSG_CONNECTION_REQUEST.getValue() &&
						(int) en.GetHashCode() != MessageTypeJava.MSG_SYNC_RETURN.getValue() &&
						!(msg instanceof EventConnChangeStatus)) {

				
					DFS.Ack(args.getMsg().getDeviceID());
				    System.out.println("SENDING ACK FROM CALLBACK...");
				}
//*********************************************************************************************************************************************************
//***Eventos 				
			// ***Generic
				if (args.getMsg().getMsgType().hashCode() == MessageTypeJava.MSG_EVENT.getValue()) {
                    if ((int)((EventMessage)args.getMsg()).getEventID() != EventMessageTypeJava.MSG_EVENT_CONN_CHANGED.getValue())
                    {
					{
                		String xMsg = "-----------------------------------------------------" +
                		"\n*** Evento " + EventMessageTypeJava.valueOf(((EventMessage)args.getMsg()).getEventID()) + " ***" +
                		"\nDeviceID.........." + ((EventMessage)args.getMsg()).getDeviceID() +
                        "\nCod evento........" + ((EventMessage)args.getMsg()).getEventID() +
                        "\nData.............." + ((EventMessage)args.getMsg()).getTimestamp().ToString() +
                        "\nDireção..........." + ((EventMessage)args.getMsg()).getAcessDirection() +
                        "\nVerão............." + ((EventMessage)args.getMsg()).getSummerTime() +
                        "\ntipo.............." + ((EventMessage)args.getMsg()).getIdentificationType() ; 
                        
                	//	byte[] btId = ((EventMessage)args.getMsg()).getIdentificationData();
                		String stB = "";
                		switch (((EventMessage)args.getMsg()).getIdentificationType()) {
						case 1:
							long idc = util.getCardId(((EventMessage)args.getMsg()).getIdentificationData());
							stB = String.valueOf(idc);
							if(idc==0)
							{
							stB = 	new String(((EventMessage)args.getMsg()).getIdentificationData());
							}														
							break;
						case 2:
							stB = new String(((EventMessage)args.getMsg()).getIdentificationData());
							break;
						default:
							stB = "";
							break;
						}                		               		
                		
                       xMsg += "\nID................" + stB+
                        
                        "\nGmt..............." + ((EventMessage)args.getMsg()).getGMT1() +
                        "\nReader............" + ((EventMessage)args.getMsg()).getReaderID() + "\n";
                		
               //Dados Adicionais	
                       if (((EventMessage)args.getMsg()).getEventID() == EventMessageTypeJava.MSG_EVENT_ACCESS_GRANTED.getValue()||
                           ((EventMessage)args.getMsg()).getEventID() == EventMessageTypeJava.MSG_EVENT_ACCESS_GRANTED_CHEAT.getValue() ||
                           ((EventMessage)args.getMsg()).getEventID() == EventMessageTypeJava.MSG_EVENT_ACCESS_GRANTED_COERCION.getValue() ||
                           ((EventMessage)args.getMsg()).getEventID() == EventMessageTypeJava.MSG_EVENT_ACCESS_GRANTED_OUT_REPOSE.getValue() || 
                           ((EventMessage)args.getMsg()).getEventID() == EventMessageTypeJava.MSG_EVENT_ACCESS_GRANTED_MASTER_CARD.getValue() )
                    	 
                       {
                    	   xMsg += "\n\nTecla pressionada........" + ((EventAccessGrantedBase)args.getMsg()).getKeyPressed() +
                           "\nMsgQuantity..............." + ((EventAccessGrantedBase)args.getMsg()).getMsgQuantity() +
                           "\nAcessCredits.............." + ((EventAccessGrantedBase)args.getMsg()).getAcessCredits() +
                           "\nCardLevel................." + ((EventAccessGrantedBase)args.getMsg()).getCardLevel();
                       }
					
                		
             //Dados adicionais caso retorno de templates registradas
                		if ((int)((EventMessage)args.getMsg()).getEventID() == (EventMessageTypeJava.MSG_EVENT_TEMPLATE_REGISTERED.getValue()))
                		{
	            			xMsg +=	"Total Templates = " + ((EventTemplateRegister)args.getMsg()).getTemplateQuantity() + "\n";
	                        for (int i = 0; i < ((EventTemplateRegister)args.getMsg()).getTemplateQuantity(); i++)
	                        {
			                   xMsg += "\nTemplate..........." + i +
				                       "\nTemplateVendor....." + ((EventTemplateRegister)args.getMsg()).getTemplate()[0].getTemplateVendor() +
				                       "\nTemplateSize......." + ((EventTemplateRegister)args.getMsg()).getTemplate()[0].getTemplateSize() +
				                       "\nTemplate..........." + ((EventTemplateRegister)args.getMsg()).getTemplate()[i].getTemplate() + "\n";
	                		}	                   
                     }
                		xMsg += "\n-----------------------------------------------------";
                		System.out.println(xMsg);
                     }					 
				}
				}
				
//*********************************************************************************************************************************************************				
//Retornos assincronos de comandos
				
				if((int)args.getMsg().getMsgType().GetHashCode()== MessageTypeJava.MSG_SYNC_RETURN.getValue())
				{
				String BuffMsg="";
	               if(
	                 args.getMsg() instanceof AutoProcessResp ||
                     args.getMsg() instanceof BlockDeviceResp ||
                     args.getMsg() instanceof CheckBioResp ||
                     args.getMsg() instanceof ConfigFirmwareResp ||
                     args.getMsg() instanceof DeletListResp ||
                     args.getMsg() instanceof DisableDigitalOutResp ||
                     args.getMsg() instanceof DisableEmergencyResp ||
                     args.getMsg() instanceof EnableDigitalOutResp ||
                     args.getMsg() instanceof EnableEmergencyResp ||
                     args.getMsg() instanceof HandkeyCalibrateResp ||
                     args.getMsg() instanceof LoadBadgeIDsResp ||
                     args.getMsg() instanceof UpdateDateTimeResp ||
                     args.getMsg() instanceof UpdateSummerTimeResp ||
                     args.getMsg() instanceof DigitalOutStatusResp ||
                     args.getMsg() instanceof DigitalInStatusResp ||
                     args.getMsg() instanceof LoadListResp ||
                     args.getMsg() instanceof ListStatusResp ||
                     args.getMsg() instanceof AlarmBackupResp ||
                     args.getMsg() instanceof DeviceStatusResp ||
                     args.getMsg() instanceof AccessBackupResp ||
                     args.getMsg() instanceof UnblockDeviceResp ||
                     args.getMsg() instanceof LoadSmartMapResp ||
                     args.getMsg() instanceof UpdateBioResp)
	               {
                       DFS.Ack(args.getMsg().getDeviceID());
                                          
                       BuffMsg +=
                    	   "\n-----------------------------------------------------\n"+
                           "\nRetorno de Comandos ***" + 
                           "\nRetorno Tipo  " + CommandReturnTypeJava.valueOf((int)((CommandReturn)args.getMsg()).getCommandId()) +
                           "\nDeviceID      " + ((CommandReturn)args.getMsg()).getDeviceId() +
                           "\nSequencia CMD " + ((CommandReturn)args.getMsg()).getCmdSeq() +
                           "\nCodigo de ret " + ((CommandReturn)args.getMsg()).getReturnCode() +
                           "\nData Ger CMD  " + ((CommandReturn)args.getMsg()).getCmdGenerationTimestamp().ToString() +
                           "\nData Exe CMD  " + ((CommandReturn)args.getMsg()).getCmdExecutionTimestamp().ToString() +
                           "\nTam dos Dados " + ((CommandReturn)args.getMsg()).getPayloadSize();
                   }
				//DeviceStatus
				if(args.getMsg() instanceof DeviceStatusResp )                	
                {
                    BuffMsg += 
                    	"\nVersion          : " + ((DeviceStatusResp)args.getMsg()).getFirmwareVersion()[0] + "." +
                                                  ((DeviceStatusResp)args.getMsg()).getFirmwareVersion()[1] + "." +
                                                  ((DeviceStatusResp)args.getMsg()).getFirmwareVersion()[2] + "." +
                                                  ((DeviceStatusResp)args.getMsg()).getFirmwareVersion()[3] +
                      "\nLastFirmwareUpdate : " + ((DeviceStatusResp)args.getMsg()).getLastFirmwareUpdate().ToString() +
                      "\nGmt                : " + ((DeviceStatusResp)args.getMsg()).getGmt() +
                      "\nLastConfUpdate     : " + ((DeviceStatusResp)args.getMsg()).getLastConfUpdate().ToString() +
                      "\nInitSummerTime     : " + ((DeviceStatusResp)args.getMsg()).getInitSummerTime() +
                      "\nEndSummerTime      : " + ((DeviceStatusResp)args.getMsg()).getEndSummerTime() +
                      "\nDeviceIP           : " + ((DeviceStatusResp)args.getMsg()).getDeviceIP()[0] + "." +
                                                  ((DeviceStatusResp)args.getMsg()).getDeviceIP()[1] + "." +
                                                  ((DeviceStatusResp)args.getMsg()).getDeviceIP()[2] + "." +
                                                  ((DeviceStatusResp)args.getMsg()).getDeviceIP()[3] +
                      "\nEmergencia         : " + ((DeviceStatusResp)args.getMsg()).getEmergency() +
                      "\nDevice Id          : " + ((DeviceStatusResp)args.getMsg()).getDeviceID() +
                      "\nReaderBlock        : " + ((DeviceStatusResp)args.getMsg()).getReaderBlock() +
                      "\n-----------------------------------------------------\n";   
                    }
				
				//Status das saidas digitais
				if(args.getMsg() instanceof DigitalInStatusResp)                	
                {
				BuffMsg +=  "\nOutputs ";
					for (int i = 0; i < ((DigitalOutStatusResp)args.getMsg()).getOutStatus().length; i++)
					{
						BuffMsg +=   ((DigitalOutStatusResp)args.getMsg()).getOutStatus()[i];
					}		
					BuffMsg += "\n-----------------------------------------------------\n";
				}				
				
                if (args.getMsg() instanceof AccessBackupResp)
                {
                  BuffMsg +=   "\nTotal Access = " + ((AccessBackupResp)args.getMsg()).getTotEvent() + "\n\n" +
                    "--------------------------------------------------------------------\n";
                    for (int i = 0, n = 1; i < (int)((AccessBackupResp)args.getMsg()).getTotEvent(); i++, n++)
                    {
                        BuffMsg += "Evento nº " + n +
                            "\nId Evento  = " + ((AccessBackupResp)args.getMsg()).getEvents()[i].getEventID() +
                                  "\nDate    = " + ((AccessBackupResp)args.getMsg()).getEvents()[i].getTimestamp() +
                                  "\nDireção = " + ((AccessBackupResp)args.getMsg()).getEvents()[i].getAcessDirection() +
                                  "\nVerão   = " + ((AccessBackupResp)args.getMsg()).getEvents()[i].getSummerTime() +
                                  "\ntipo    = " + ((AccessBackupResp)args.getMsg()).getEvents()[i].getIdentificationType() +
                                  "\nid      = " + ((AccessBackupResp)args.getMsg()).getEvents()[i].getIdentificationData() +
                                  "\nGmt     = " + ((AccessBackupResp)args.getMsg()).getEvents()[i].getGMT1() +
                                  "\nReader  = " + ((AccessBackupResp)args.getMsg()).getEvents()[i].getReaderID() + " \n " +
                               "--------------------------------------------------------------------\n";
                    }
                }
                
                if(args.getMsg() instanceof AutoProcessResp)
                {
                    BuffMsg +=
                        "\nTotProcess   " + ((AutoProcessResp)args.getMsg()).getTotProcess() +
                        "\n--------------------------------------------------------\n";
           
                        for(int i = 0;i < ((AutoProcessResp)args.getMsg()).getTotProcess();i++ )
                        {
                            BuffMsg += "\nProcessCod   " + ((AutoProcessResp)args.getMsg()).getProcess()[i].getProcessCode()[1] +
                                       "\nDta da ultima Execução" + ((AutoProcessResp)args.getMsg()).getProcess()[i].getExeDateTime().ToString() +
                                         "\n--------------------------------------------------------\n";
                        }
                }	
                
			if(!BuffMsg.contentEquals(""))System.out.println(BuffMsg);
			
			}					

//*********************************************************************************************************************************************************
//Respostas a  requisição de pessoa
				if ((int) args.getMsg().getMsgType().hashCode() == MessageTypeJava.MSG_ACCESS_REQUEST_PERSON.getValue()) {
					byte[] personId = (byte[]) ((AccessRequestPerson) args.getMsg()).getPersonId();

					System.out.println("-----------------------------------------------------" +
							"\nRequisição de Pessoa:." +
                            "\nCardID..........................." +	new String(personId) +
                            "\nDeviceID........................." + ((AccessRequestPerson)args.getMsg()).getDeviceID() +
                            "\nReaderID........................." + ((AccessRequestPerson)args.getMsg()).getReaderId() +
                            "\nData............................." + ((AccessRequestPerson)args.getMsg()).getTimestampRequest() + 
                            "\n-----------------------------------------------------\n");
					
					ResponseAccessValid rav = new ResponseAccessValid();
					
					rav.setDeviceID(args.getMsg().getDeviceID());
					rav.setFrisk((byte)0);
					rav.setCreditControlType((byte)0);
					rav.setPassword("123456");
                    rav.setDataUpdateFlag((byte)0);
                    rav.setCreditControlType((byte)0);
                    rav.setDeviceID(args.getMsg().getDeviceID());
                    rav.setActualLevel((byte)0);
                    rav.setTimeScheduleCredit((byte)1);
                    rav.setBiometricLevel((byte)0);
                    rav.setUserMessage(String.format("%32s", "Welcome").getBytes());

					ResponsePersonStructure rts = new ResponsePersonStructure();
						rts.setEventStructure(rav);
						rts.setAppConnectionStatus((byte) 2);
						rts.setDeviceID(args.getMsg().getDeviceID());
						rts.setUserMessage(String.format("%32s", "Welcome").getBytes());
						

					DFS.Execute(rts);
				}
//*********************************************************************************************************************************************************
//requisição de cartão
				if (MessageTypeJava.valueOf(args.getMsg().getMsgType().hashCode())== MessageTypeJava.MSG_ACCESS_REQUEST_CARD ){
					byte[] cardId = (byte[]) ((AccessRequestCard) args.getMsg()).getCardId();
				
					System.out.println("-----------------------------------------------------" +
							"\nRequisição de Cartão:." +
                            "\nCardID..........................." + util.getCardId(cardId) + //ConvertHexString2Value(cardId) +
                            "\nDeviceID........................." + ((AccessRequestCard)args.getMsg()).getDeviceID() +
                            "\nReaderID........................." + ((AccessRequestCard)args.getMsg()).getReaderId() +
                            "\nData............................." + ((AccessRequestCard)args.getMsg()).getTimestampRequest() 
                            +"\n-----------------------------------------------------\n");

					ResponseAccessValid rav = new ResponseAccessValid();
						rav.setDeviceID(args.getMsg().getDeviceID());
						rav.setFrisk((byte)0);
						rav.setCreditControlType((byte)0);
						rav.setPassword("123456");
	                    rav.setDataUpdateFlag((byte)0);
	                    rav.setCreditControlType((byte)0);
	                    rav.setDeviceID(args.getMsg().getDeviceID());
	                    rav.setActualLevel((byte)0);
	                    rav.setTimeScheduleCredit((byte)1);
	                    rav.setBiometricLevel((byte)0);
	                    rav.setUserMessage(String.format("%32s", "Welcome").getBytes());
	            					
					//Pendências	
	                //Penência de faixa horária    
					PendingData[] pdt = new PendingData[1];
						pdt[0] = new PendingData();
						pdt[0].setData(new byte[]{0x01,0x02});
						pdt[0].setDeviceID(args.getMsg().getDeviceID());
						pdt[0].setFieldID((byte)35);
						
						int inicio = 480; //Início da permissao 8:00 
						int fim	= 1320;	//Termino da permissao 22:00  
												
	                    byte[] ranges = new byte[21];
	                    ranges[0] = (byte)(inicio & 0X00FF);
	                    ranges[1] = (byte)(((inicio & 0X0F00) >> 4) | (fim & 0X0F00) >> 8);
	                    ranges[2] = (byte)(fim & 0X0FF);
	                    //Demais faixas = 0, 
	                    for (int i = 3; i < ranges.length; i++) ranges[i] = 0;
					
					PendingStructure pds = new PendingStructure();
						pds.setPendingData(pdt);							
						pds.setLastUpdate(DateTime.getNow());
						pds.setDeviceID(args.getMsg().getDeviceID());					
                  
					//resposta
					ResponseTagStructure rts = new ResponseTagStructure();                    
						rts.setEventStructure(rav);
						rts.setPendingStructure(pds);
						rts.setPendingStructSize(pds.getBytes().length); //tamanho da pendencias
						rts.setPersonId(String.format("%23s", "0123ABC123454").getBytes());
						rts.setPersonlogicalId(((AccessRequestCard)args.getMsg()).getCardId());
						rts.setCardTech((byte)3);
						rts.setCardType((byte)1);		                
						rts.setResponseSize(1);						
						rts.setAppConnectionStatus((byte)2);
						rts.setDeviceID(args.getMsg().getDeviceID());
						rts.setUserMessage(String.format("%32s", "Welcome").getBytes());
						
					DFS.Execute(rts);
				}	
			}
		});

		LogWriter.addLogHandler(new WriteLogHandler() {
			@Override
			public void Invoke(WriteLogArgs arg0) {
				System.out.println(arg0.getLog());
			}
		});

		
		
		Util util = new Util();
		long cad =	util.getCardId(new byte[]{0x00,(byte)0xFF,0x4D,0x0D,0x69});
		
		// Ip e porta do servidor
		DFS.Start("10.104.1.156", 3232, 2);
		Thread.sleep(10000);

	    DateFormat dateFormat = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
	    Date date = new Date();
		UpdateDateTime updt = new UpdateDateTime();
		updt.setTime(dateFormat.format(date).toString());
		updt.setCommStatus((byte) 2);
		updt.setDeviceID(3);
		updt.setSeqCmd(001);

//	  DFS.Execute(updt);
	 

		EnableEmergency bd = new EnableEmergency();
		bd.setCommStatus((byte) 2);
		bd.setDeviceID(5);
		bd.setSeqCmd(157);
		bd.setType((byte) 0);
		// DFS.Execute(bd);

		 Thread.sleep(10000);

		DisableEmergency ud = new DisableEmergency();
		ud.setCommStatus((byte) 1);
		ud.setDeviceID(5);
		ud.setSeqCmd(1);
		ud.setType((byte) 0);
		// DFS.Execute(ud);
		
		Session[]  sess = DFS.Session();
		
		System.out.println("\n\n Dev stat \n");
		for (Session deviceInf : sess) 
		{
			System.out.println("Ip " + deviceInf.getIpDevice());
			
		}

		//Envio de configuração via path do arquivo de configuração
		String filePath = "C:\\Temp\\3.1.0_126_2_1100_BIO.xml";
		DeviceConfig confDev = new DeviceConfig();
		confDev.setDeviceID(2);
		confDev.setCommStatus((byte)2);
		confDev.setSeqCmd(123);
		confDev.Configuration(filePath);
		
		DFS.Execute(confDev);
		
		
		System.out.println("\n\n");
		
	//	Thread.sleep(10000);

		DigitalOutStatus gos = new DigitalOutStatus();
		gos.setCommStatus((byte) 2);
		gos.setDeviceID(2);
		gos.setSeqCmd(125);
	//	DFS.Execute(gos);
		
	//	System.out.println("\n Enviado DigitalOutStatus \n");

		Thread.sleep(1000);
		
		DeviceStatus dv = new DeviceStatus();
		dv.setCommStatus((byte) 2);
		dv.setDeviceID(2);
		dv.setSeqCmd(125);
//		DFS.Execute(dv);
		Thread.sleep(50000);

		
//-----------------------------------------------------------------------------------------------------------------------
	ConfigFirmware configFirmware = new ConfigFirmware();
	
//	configFirmware.setNewDeviceIP("10.104.5.126");
//	configFirmware.setConnMode(0);
//	configFirmware.setNewNetMask("255.255.0.0");
//	configFirmware.setNewNetGw("10.104.1.10");
//	configFirmware.setServerIP("10.104.1.156");
//	configFirmware.setInitSummerTime("0010");
//	configFirmware.setEndSummerTime("0020");
//	configFirmware.setUtc((short) -3);
//	configFirmware.setSubsidiaryID((short) 0);
//	configFirmware.setStandardMsg("hello");
//	configFirmware.setUseDisplay(1);
//	configFirmware.setKeyBlock(0);
//	configFirmware.setCardExpirationDays((short) 0);
//	configFirmware.setKeepAlive(5000);
//	configFirmware.setCommTimeout(3000);
//	
//	
//	Function[] function = new Function[10];
//	for(int i =0; i < function.length; i++) function[i] = new Function();
//	
//	function[0].setExeActuator("1;0");
//	
//	configFirmware.setFunctions(function);
//	
//	Input[] input = new Input[4];
//	for(int i =0; i < input.length; i++) input[i] = new Input();
//	
//	
//	
//	Reader[] readers = new Reader[22];
//	for(int i =0; i < readers.length; i++) readers[i] = new Reader();
	
//	Actuate emergencyActuator = new Actuate();
//	
//	Actuate[] actuate = new Actuate[4];
//	for(int i =0; i < actuate.length; i++) actuate[i] = new Actuate();
//	
//	
//	EventMessageScreen[] eventMessageScreen = new EventMessageScreen[32];
//	for(int i =0; i < eventMessageScreen .length; i++) eventMessageScreen[i] = new EventMessageScreen();
//	
//	
//	configFirmware.setAuthorizerTimeout(0);
//	configFirmware.setSmartCardValidation(1);
//	configFirmware.setEnclosureCode(0);
////	configFirmware.setCertExpBDCC(new byte[]);
//	configFirmware.setCommStatus((byte) 2);
//	configFirmware.setSeqCmd(123);
//	configFirmware.setDeviceID(1);
	
		
//-----------------------------------------------------------------------------------------------------------------------		
			
		
//		Thread.sleep(1000000);

	while (true) {
				
	}
		
		// DFS.Stop();

	}

	public static void main(String[] args) {
		DFSTest app = new DFSTest();
		try {
			app.process();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

}
