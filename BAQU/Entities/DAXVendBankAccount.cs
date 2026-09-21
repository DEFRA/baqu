using System.ComponentModel.DataAnnotations.Schema;

namespace BAQU.Entities;
public class RSFDwhVendVendorBankAccountStaging
{
    public RSFDwhVendVendorBankAccountStaging()
    {
        CROSSRATE = 0.0m;
        ISDEFAULTBANKACCOUNTENUM = 0;
        ADDRESSCOUNTRYISOCODE = "";
        ISDEFAULTBANKACCOUNT = 0;
        ROUTINGNUMBERTYPE = 0;
        DEFINITIONGROUP = "";
        EXECUTIONID = "";
        ISSELECTED = 0;
        ACCOUNTID = "";
        ACCOUNTNUM = "";
        BANKNAMEINKANA = "";
        BANKGROUPID = "";
        ROUTINGNUMBERTYPEENUM = 0;
        REGISTRATIONNUM = "";
        DUNSNUMBER = "";
        DUNS4NUMBERSUFFIX = "";
        CONTROLINTERNALNUMBER = "";
        SWIFTNO = "";
        BANKIBAN = "";
        CORRESPONDENCEBANKACCOUNTNUMBER = "";
        BANKCONSTANTSYMBOL = "";
        BANKSPECIFICSYMBOL = "";
        ACTIVEDATE = new DateTime(1900, 1, 1);
        FOREIGNBANKGROUPID = "";
        FOREIGNBANKACCOUNTNUMBER = "";
        FOREIGNBANKSWIFTCODE = "";
        RECIPIENTTEXTCODE = "";
        BANKMESSAGE = "";
        RATEOFEXCHANGEREFERENCE = "";
        CURRENCYCODE = "";
        BANKCORRESPONDENCEBANKGROUPID = "";
        BANKCORRESPONDENCEACCOUNTBANKGROUPID = "";
        INTERIMBANKCORRESPONDENCEBANKACCOUNTNUMBER = "";
        INTERIMVENDORBANKACCOUNTNUMBER = "";
        CONTACTPHONENUMBER = "";
        CONTACTPHONENUMBEREXTENSION = "";
        CONTACTMOBILEPHONENUMBER = "";
        CONTACTPAGER = "";
        CONTACTFAXNUMBER = "";
        CONTACTEMAILADDRESS = "";
        CONTACTEMAILADDRESSFORSENDINGSMS = "";
        CONTACTINTERNETADDRESS = "";
        CONTACTTELEXNUMBER = "";
        CONTACTNAME = "";
        FORMATTEDADDRESS = "";
        ADDRESSLOCATIONID = "";
        ADDRESSDESCRIPTION = "";
        ADDRESSCOUNTRY = "";
        ADDRESSSTATE = "";
        ADDRESSCITY = "";
        ADDRESSCOUNTY = "";
        ADDRESSSTREET = "";
        ADDRESSSTREETNUMBER = "";
        ADDRESSCITYINKANA = "";
        ADDRESSSTREETINKANA = "";
        ADDRESSLATITUDE = 0.0m;
        ADDRESSLONGITUDE = 0.0m;
        ADDRESSZIPCODE = "";
        ADDRESSDISTRICTNAME = "";
        ADDRESSPOSTBOX = "";
        ADDRESSBUILDINGCOMPLIMENT = "";
        ADDRESSVALIDFROM = new DateTime(1900, 1, 1);
        ADDRESSVALIDTO = new DateTime(1900, 1, 1);
        ADDRESSTIMEZONE = 0;
        RSFBANKROLLNUMBER = "";
        RSFSKIPBANKHOLDENUM = 0;
        RSFRELEASEDBANKHOLDENUM = 0;
        RSFDWHSOURCERECID = 0;
        RSFDWHSOURCEDATAAREAID = "";
        RSFDWHCREATEDBY = "";
        RSFDWHCREATEDDATETIME = new DateTime(1900, 1, 1);
        RSFSKIPBANKHOLD = 0;
        RSFRELEASEDBANKHOLD = 0;
        PARTITION = "";
        TRANSFERSTATUS = 0;
        DATAAREAID = "";
        SYNCSTARTDATETIME = new DateTime(1900, 1, 1);
    }

    [Column("CROSSRATE")]
    public decimal CROSSRATE { get; set; }

    [Column("ISDEFAULTBANKACCOUNTENUM")]
    public int ISDEFAULTBANKACCOUNTENUM { get; set; }

    [Column("ADDRESSCOUNTRYISOCODE")]
    public string ADDRESSCOUNTRYISOCODE { get; set; } = default!;

    [Column("ISDEFAULTBANKACCOUNT")]
    public int ISDEFAULTBANKACCOUNT { get; set; }

    [Column("ROUTINGNUMBERTYPE")]
    public int ROUTINGNUMBERTYPE { get; set; }

    [Column("DEFINITIONGROUP")]
    public string DEFINITIONGROUP { get; set; } = default!;

    [Column("EXECUTIONID")]
    public string EXECUTIONID { get; set; } = default!;

    [Column("ISSELECTED")]
    public int ISSELECTED { get; set; }

    [Column("ACCOUNTID")]
    public string ACCOUNTID { get; set; } = default!;

    [Column("ACCOUNTNUM")]
    public string ACCOUNTNUM { get; set; } = default!;

    [Column("NAME")]
    public string BUSINESSNAME { get; set; } = default!;

    [Column("BANKNAMEINKANA")]
    public string BANKNAMEINKANA { get; set; } = default!;

    [Column("BANKGROUPID")]
    public string BANKGROUPID { get; set; } = default!;

    [Column("VENDACCOUNT")]
    public string VENDACCOUNT { get; set; } = default!;

    [Column("ROUTINGNUMBERTYPEENUM")]
    public int ROUTINGNUMBERTYPEENUM { get; set; }

    [Column("REGISTRATIONNUM")]
    public string REGISTRATIONNUM { get; set; } = default!;

    [Column("DUNSNUMBER")]
    public string DUNSNUMBER { get; set; } = default!;

    [Column("DUNS4NUMBERSUFFIX")]
    public string DUNS4NUMBERSUFFIX { get; set; } = default!;

    [Column("CONTROLINTERNALNUMBER")]
    public string CONTROLINTERNALNUMBER { get; set; } = default!;

    [Column("SWIFTNO")]
    public string SWIFTNO { get; set; } = default!;

    [Column("BANKIBAN")]
    public string BANKIBAN { get; set; } = default!;

    [Column("CORRESPONDENCEBANKACCOUNTNUMBER")]
    public string CORRESPONDENCEBANKACCOUNTNUMBER { get; set; } = default!;

    [Column("BANKCONSTANTSYMBOL")]
    public string BANKCONSTANTSYMBOL { get; set; } = default!;

    [Column("BANKSPECIFICSYMBOL")]
    public string BANKSPECIFICSYMBOL { get; set; } = default!;

    [Column("ACTIVEDATE")]
    public DateTime ACTIVEDATE { get; set; }

    [Column("EXPIRYDATE")]
    public DateTime EXPIRYDATE { get; set; }

    [Column("FOREIGNBANKGROUPID")]
    public string FOREIGNBANKGROUPID { get; set; } = default!;

    [Column("FOREIGNBANKACCOUNTNUMBER")]
    public string FOREIGNBANKACCOUNTNUMBER { get; set; } = default!;

    [Column("FOREIGNBANKSWIFTCODE")]
    public string FOREIGNBANKSWIFTCODE { get; set; } = default!;

    [Column("RECIPIENTTEXTCODE")]
    public string RECIPIENTTEXTCODE { get; set; } = default!;

    [Column("BANKMESSAGE")]
    public string BANKMESSAGE { get; set; } = default!;

    [Column("RATEOFEXCHANGEREFERENCE")]
    public string RATEOFEXCHANGEREFERENCE { get; set; } = default!;

    [Column("CURRENCYCODE")]
    public string CURRENCYCODE { get; set; } = default!;

    [Column("BANKCORRESPONDENCEBANKGROUPID")]
    public string BANKCORRESPONDENCEBANKGROUPID { get; set; } = default!;

    [Column("BANKCORRESPONDENCEACCOUNTBANKGROUPID")]
    public string BANKCORRESPONDENCEACCOUNTBANKGROUPID { get; set; } = default!;

    [Column("INTERIMBANKCORRESPONDENCEBANKACCOUNTNUMBER")]
    public string INTERIMBANKCORRESPONDENCEBANKACCOUNTNUMBER { get; set; } = default!;

    [Column("INTERIMVENDORBANKACCOUNTNUMBER")]
    public string INTERIMVENDORBANKACCOUNTNUMBER { get; set; } = default!;

    [Column("CONTACTPHONENUMBER")]
    public string CONTACTPHONENUMBER { get; set; } = default!;

    [Column("CONTACTPHONENUMBEREXTENSION")]
    public string CONTACTPHONENUMBEREXTENSION { get; set; } = default!;

    [Column("CONTACTMOBILEPHONENUMBER")]
    public string CONTACTMOBILEPHONENUMBER { get; set; } = default!;

    [Column("CONTACTPAGER")]
    public string CONTACTPAGER { get; set; } = default!;

    [Column("CONTACTFAXNUMBER")]
    public string CONTACTFAXNUMBER { get; set; } = default!;

    [Column("CONTACTEMAILADDRESS")]
    public string CONTACTEMAILADDRESS { get; set; } = default!;

    [Column("CONTACTEMAILADDRESSFORSENDINGSMS")]
    public string CONTACTEMAILADDRESSFORSENDINGSMS { get; set; } = default!;

    [Column("CONTACTINTERNETADDRESS")]
    public string CONTACTINTERNETADDRESS { get; set; } = default!;

    [Column("CONTACTTELEXNUMBER")]
    public string CONTACTTELEXNUMBER { get; set; } = default!;

    [Column("CONTACTNAME")]
    public string CONTACTNAME { get; set; } = default!;

    [Column("FORMATTEDADDRESS")]
    public string FORMATTEDADDRESS { get; set; } = default!;

    [Column("ADDRESSLOCATIONID")]
    public string ADDRESSLOCATIONID { get; set; } = default!;

    [Column("ADDRESSDESCRIPTION")]
    public string ADDRESSDESCRIPTION { get; set; } = default!;

    [Column("ADDRESSCOUNTRY")]
    public string ADDRESSCOUNTRY { get; set; } = default!;

    [Column("ADDRESSSTATE")]
    public string ADDRESSSTATE { get; set; } = default!;

    [Column("ADDRESSCITY")]
    public string ADDRESSCITY { get; set; } = default!;

    [Column("ADDRESSCOUNTY")]
    public string ADDRESSCOUNTY { get; set; } = default!;

    [Column("ADDRESSSTREET")]
    public string ADDRESSSTREET { get; set; } = default!;

    [Column("ADDRESSSTREETNUMBER")]
    public string ADDRESSSTREETNUMBER { get; set; } = default!;

    [Column("ADDRESSCITYINKANA")]
    public string ADDRESSCITYINKANA { get; set; } = default!;

    [Column("ADDRESSSTREETINKANA")]
    public string ADDRESSSTREETINKANA { get; set; } = default!;

    [Column("ADDRESSLATITUDE")]
    public decimal ADDRESSLATITUDE { get; set; }

    [Column("ADDRESSLONGITUDE")]
    public decimal ADDRESSLONGITUDE { get; set; }

    [Column("ADDRESSZIPCODE")]
    public string ADDRESSZIPCODE { get; set; } = default!;

    [Column("ADDRESSDISTRICTNAME")]
    public string ADDRESSDISTRICTNAME { get; set; } = default!;

    [Column("ADDRESSPOSTBOX")]
    public string ADDRESSPOSTBOX { get; set; } = default!;

    [Column("ADDRESSBUILDINGCOMPLIMENT")]
    public string ADDRESSBUILDINGCOMPLIMENT { get; set; } = default!;

    [Column("ADDRESSVALIDFROM")]
    public DateTime ADDRESSVALIDFROM { get; set; }

    [Column("ADDRESSVALIDTO")]
    public DateTime ADDRESSVALIDTO { get; set; }

    [Column("ADDRESSTIMEZONE")]
    public int ADDRESSTIMEZONE { get; set; }

    [Column("RSFBANKROLLNUMBER")]
    public string RSFBANKROLLNUMBER { get; set; } = default!;

    [Column("RSFSKIPBANKHOLDENUM")]
    public int RSFSKIPBANKHOLDENUM { get; set; }

    [Column("RSFRELEASEDBANKHOLDENUM")]
    public int RSFRELEASEDBANKHOLDENUM { get; set; }

    [Column("RSFDWHSOURCERECID")]
    public long RSFDWHSOURCERECID { get; set; }

    [Column("RSFDWHSOURCEDATAAREAID")]
    public string RSFDWHSOURCEDATAAREAID { get; set; } = default!;

    [Column("RSFDWHCREATEDBY")]
    public string RSFDWHCREATEDBY { get; set; } = default!;

    [Column("RSFDWHCREATEDDATETIME")]
    public DateTime RSFDWHCREATEDDATETIME { get; set; }

    [Column("RSFDWHMODIFIEDBY")]
    public string MODIFIEDBY { get; set; } = default!;

    [Column("RSFDWHMODIFIEDDATETIME")]
    public DateTime MODIFIEDDATETIME { get; set; }

    [Column("RSFSKIPBANKHOLD")]
    public int RSFSKIPBANKHOLD { get; set; }

    [Column("RSFRELEASEDBANKHOLD")]
    public int RSFRELEASEDBANKHOLD { get; set; }

    [Column("PARTITION")]
    public string PARTITION { get; set; } = default!;

    [Column("TRANSFERSTATUS")]
    public int TRANSFERSTATUS { get; set; }

    [Column("DATAAREAID")]
    public string DATAAREAID { get; set; } = default!;

    [Column("SYNCSTARTDATETIME")]
    public DateTime SYNCSTARTDATETIME { get; set; }
}


