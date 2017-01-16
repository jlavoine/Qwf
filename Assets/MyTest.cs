using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public class MyTest : MonoBehaviour {

    public interface TestInterface {
        int GetA();
        string GetB();
    }

    public class TestClass : TestInterface {
        public int A;
        public string B;

        public int GetA() { return A; }
        public string GetB() { return B; }
    }

    // Use this for initialization
    void Start() {
        UnityEngine.Debug.LogError( "Starting" );
        TestInterface test1 = new TestClass() { A = 5, B = "Hi" };
        string serial = JsonConvert.SerializeObject( test1 );
        UnityEngine.Debug.LogError( serial );

        TestInterface test2 = JsonConvert.DeserializeObject<TestClass>( serial );
        Debug.LogError( test2.GetA() );
    }

    // Update is called once per frame
    void Update() {

    }
}
