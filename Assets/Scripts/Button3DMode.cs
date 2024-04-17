using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckboxHandler : MonoBehaviour
{
    public Toggle checkbox;

    // Καλείται όταν πατηθεί το κουμπί
    public void OnButtonClick()
    {
        // Ελέγχουμε αν το checkbox είναι επιλεγμένο ή όχι
        if (checkbox != null)
        {
            // Αν είναι επιλεγμένο, κάνουμε το checkbox μαύρο ή εμφανίζουμε το τικ
            if (checkbox.isOn)
            {
                // Κάνε το checkbox μαύρο ή εμφάνισε το τικ
                // Προσαρμόστε ανάλογα με το τι θέλετε να κάνετε εδώ
                // Για παράδειγμα, αν έχετε επιλέξει το μαύρο, χρησιμοποιήστε το παρακάτω:
                checkbox.image.color = Color.black;
            }
            else
            {
                // Κάντε το checkbox να φαίνεται όπως συνήθως
                // Εδώ μπορείτε να επαναφέρετε το χρώμα του checkbox στο default του
                checkbox.image.color = Color.white;
            }
        }
    }
}

public class Button3DMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
