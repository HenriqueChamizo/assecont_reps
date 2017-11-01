import java.io.File;
import java.io.IOException;
import net.sf.jni4net.Bridge;

//import net.sf.jni4net.Bridge;

public class LoadDFS {
	
	public LoadDFS(String dll) {
		// TODO Auto-generated method stub
	    try {
			Bridge.init();
			Bridge.LoadAndRegisterAssemblyFrom(new File(dll));
			System.out.println("Loaded");
		} catch (IOException e) {
			e.printStackTrace();
		}   
	}
	

}
