using System;

namespace Berke.Marcas.WebUI.Helpers {

	using System.Resources;
	using System.Globalization;
	using System.Diagnostics;
	using System.IO;
	using System.Reflection;
	/// <summary>
	/// Summary description for Resources.
	/// </summary>
	public class Resources {

		private const string RESOURCE_FILE = ".RM";

		public enum Keys {
			/// <summary>
			/// Message key for resource retrival.
			/// </summary>
			LoginWelcome,
			LoginIncorrect,
			LoginLogin,
			LoginPwd,
			HomePublic,
			HomePrivate,
			LoginLogout,
			AccountSummary,
			TransactionLog,
			CashAccounts,
			AccountIdentifier,
			BalanceFcy,
			BalanceLcy,
			AccountType,
			CreditCards,
			CreditCardCloseDate,
			DueDate,
			DateFrom,
			DateTo,
			Search,
			DateRequired,
			DateGreaterOrEqualThanToday,
			CreditCardName,
			CreditCardNumber,
			CreditCardType,
			Bills,
			BillIdentifier,
			TermDeposits,
			TermDepositActionOnDueDate,
			Currency,
			Interest,
			Principal,
			Tax,
			Term,
			TermDepositIdentifier,
			InvestmentFunds,
			InvestmentFundName,
			Quote,
			Shares,
			PersonalizationOtherAccountsTopLabel,
			Description,
			Add,
			Delete,
			Select,
			Close,
			Prepare,
			Edit,
			Confirm,
			Done,
			AccountDescription,
			FundsTransfer,
			FundsTransferPreparation,
			FundsTransferConfirmation,
			FundsTransferReceipt,
			FundsTransferMustSelectFromAccount,
			FundsTransferMustSelectToAccount,
			AccountNewBalance,
			AccountFrom,
			AccountTo,
			Amount,
			EffectiveOn,
			Comments,
			AccountsNotTheSame,
			AccountsOwn,
			AccountsOthers,
			TransactionCompleted,
			TransactionCompletedSuccesfully,
			BillSubscriptionManagement,
			BillPaymentReceipt,
			HomePromotion,
			HomeSecondColumn,
			BillPaymentPreparation,
			BillPaymentConfirmation,
			BillPaymentMustSelectFromAccount,
			BillSubscriptionPreparation,
			BillSubscriptionConfirmation,
			BillSubscriptionReceipt,
			BillSubscriptionMustSelectProduct,
			BillSubscriptionMustEnterAccountNumber,
			BillSubscriptionInvalidAccountNumber,
			ConfirmationPrompt,
			Bill,
			BillProduct,
			BillPayment,
			BillSubscription,
			BillUnsubscription,
			BillsPending,
			BillProductExternalAccountTooltip,
			BillProductExternalAccountIdentifier,
			BillProductExternalAccount,
			Subscribed,
			Help,
			Home,
			NewUser,
			EnrollNow,
			ForgotYourID,
			ForgotYourPwd,
			ErrorUnknown,
			ErrorGeneralMsg,
			LowerLimit,
			Today,
			AmountShoulBePositive,
			AmountRequired,
			Resume,
			Back,
			FxRate,
			BalanceUsd,
			InvalidSubscription,
			BillPaymentMustSelectBill,
			PleaseSelect,
			AccountFormat,
			AmountAndFxRateFormat,
			BillAmountExceedsFunds,
			BillAmountExceedsLimit,
			AccountNotFound,
			AccountFormatOther,
			ErrorMessagePattern,
			InsufficientFunds,
			LoginGoButtonImage,
			LoginHeaderImage,
			LoginFooterImage,
			CalendarIcon,
			GlobalBankBanner,

		}

		static Resources() {
			string baseName = String.Concat( typeof( Resources ).Namespace, RESOURCE_FILE );
			_rm = new System.Resources.ResourceManager( baseName, Assembly.GetExecutingAssembly() );
		}

		static private System.Resources.ResourceManager _rm;

				static public string LoginWelcome {
			get {
				return _rm.GetString( Keys.LoginWelcome.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string LoginIncorrect {
			get {
				return _rm.GetString( Keys.LoginIncorrect.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string LoginLogin {
			get {
				return _rm.GetString( Keys.LoginLogin.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string LoginPwd {
			get {
				return _rm.GetString( Keys.LoginPwd.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string HomePublic {
			get {
				return _rm.GetString( Keys.HomePublic.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string HomePrivate {
			get {
				return _rm.GetString( Keys.HomePrivate.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string LoginLogout {
			get {
				return _rm.GetString( Keys.LoginLogout.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AccountSummary {
			get {
				return _rm.GetString( Keys.AccountSummary.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string TransactionLog {
			get {
				return _rm.GetString( Keys.TransactionLog.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string CashAccounts {
			get {
				return _rm.GetString( Keys.CashAccounts.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AccountIdentifier {
			get {
				return _rm.GetString( Keys.AccountIdentifier.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BalanceFcy {
			get {
				return _rm.GetString( Keys.BalanceFcy.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BalanceLcy {
			get {
				return _rm.GetString( Keys.BalanceLcy.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AccountType {
			get {
				return _rm.GetString( Keys.AccountType.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string CreditCards {
			get {
				return _rm.GetString( Keys.CreditCards.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string CreditCardCloseDate {
			get {
				return _rm.GetString( Keys.CreditCardCloseDate.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string DueDate {
			get {
				return _rm.GetString( Keys.DueDate.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string DateFrom {
			get {
				return _rm.GetString( Keys.DateFrom.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string DateTo {
			get {
				return _rm.GetString( Keys.DateTo.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Search {
			get {
				return _rm.GetString( Keys.Search.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string DateRequired {
			get {
				return _rm.GetString( Keys.DateRequired.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string DateGreaterOrEqualThanToday {
			get {
				return _rm.GetString( Keys.DateGreaterOrEqualThanToday.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string CreditCardName {
			get {
				return _rm.GetString( Keys.CreditCardName.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string CreditCardNumber {
			get {
				return _rm.GetString( Keys.CreditCardNumber.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string CreditCardType {
			get {
				return _rm.GetString( Keys.CreditCardType.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Bills {
			get {
				return _rm.GetString( Keys.Bills.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillIdentifier {
			get {
				return _rm.GetString( Keys.BillIdentifier.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string TermDeposits {
			get {
				return _rm.GetString( Keys.TermDeposits.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string TermDepositActionOnDueDate {
			get {
				return _rm.GetString( Keys.TermDepositActionOnDueDate.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Currency {
			get {
				return _rm.GetString( Keys.Currency.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Interest {
			get {
				return _rm.GetString( Keys.Interest.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Principal {
			get {
				return _rm.GetString( Keys.Principal.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Tax {
			get {
				return _rm.GetString( Keys.Tax.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Term {
			get {
				return _rm.GetString( Keys.Term.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string TermDepositIdentifier {
			get {
				return _rm.GetString( Keys.TermDepositIdentifier.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string InvestmentFunds {
			get {
				return _rm.GetString( Keys.InvestmentFunds.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string InvestmentFundName {
			get {
				return _rm.GetString( Keys.InvestmentFundName.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Quote {
			get {
				return _rm.GetString( Keys.Quote.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Shares {
			get {
				return _rm.GetString( Keys.Shares.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string PersonalizationOtherAccountsTopLabel {
			get {
				return _rm.GetString( Keys.PersonalizationOtherAccountsTopLabel.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Description {
			get {
				return _rm.GetString( Keys.Description.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Add {
			get {
				return _rm.GetString( Keys.Add.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Delete {
			get {
				return _rm.GetString( Keys.Delete.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Select {
			get {
				return _rm.GetString( Keys.Select.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Close {
			get {
				return _rm.GetString( Keys.Close.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Prepare {
			get {
				return _rm.GetString( Keys.Prepare.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Edit {
			get {
				return _rm.GetString( Keys.Edit.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Confirm {
			get {
				return _rm.GetString( Keys.Confirm.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Done {
			get {
				return _rm.GetString( Keys.Done.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AccountDescription {
			get {
				return _rm.GetString( Keys.AccountDescription.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string FundsTransfer {
			get {
				return _rm.GetString( Keys.FundsTransfer.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string FundsTransferPreparation {
			get {
				return _rm.GetString( Keys.FundsTransferPreparation.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string FundsTransferConfirmation {
			get {
				return _rm.GetString( Keys.FundsTransferConfirmation.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string FundsTransferReceipt {
			get {
				return _rm.GetString( Keys.FundsTransferReceipt.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string FundsTransferMustSelectFromAccount {
			get {
				return _rm.GetString( Keys.FundsTransferMustSelectFromAccount.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string FundsTransferMustSelectToAccount {
			get {
				return _rm.GetString( Keys.FundsTransferMustSelectToAccount.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AccountNewBalance {
			get {
				return _rm.GetString( Keys.AccountNewBalance.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AccountFrom {
			get {
				return _rm.GetString( Keys.AccountFrom.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AccountTo {
			get {
				return _rm.GetString( Keys.AccountTo.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Amount {
			get {
				return _rm.GetString( Keys.Amount.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string EffectiveOn {
			get {
				return _rm.GetString( Keys.EffectiveOn.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Comments {
			get {
				return _rm.GetString( Keys.Comments.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AccountsNotTheSame {
			get {
				return _rm.GetString( Keys.AccountsNotTheSame.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AccountsOwn {
			get {
				return _rm.GetString( Keys.AccountsOwn.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AccountsOthers {
			get {
				return _rm.GetString( Keys.AccountsOthers.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string TransactionCompleted {
			get {
				return _rm.GetString( Keys.TransactionCompleted.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string TransactionCompletedSuccesfully {
			get {
				return _rm.GetString( Keys.TransactionCompletedSuccesfully.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillSubscriptionManagement {
			get {
				return _rm.GetString( Keys.BillSubscriptionManagement.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillPaymentReceipt {
			get {
				return _rm.GetString( Keys.BillPaymentReceipt.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string HomePromotion {
			get {
				return _rm.GetString( Keys.HomePromotion.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string HomeSecondColumn {
			get {
				return _rm.GetString( Keys.HomeSecondColumn.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillPaymentPreparation {
			get {
				return _rm.GetString( Keys.BillPaymentPreparation.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillPaymentConfirmation {
			get {
				return _rm.GetString( Keys.BillPaymentConfirmation.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillPaymentMustSelectFromAccount {
			get {
				return _rm.GetString( Keys.BillPaymentMustSelectFromAccount.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillSubscriptionPreparation {
			get {
				return _rm.GetString( Keys.BillSubscriptionPreparation.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillSubscriptionConfirmation {
			get {
				return _rm.GetString( Keys.BillSubscriptionConfirmation.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillSubscriptionReceipt {
			get {
				return _rm.GetString( Keys.BillSubscriptionReceipt.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillSubscriptionMustSelectProduct {
			get {
				return _rm.GetString( Keys.BillSubscriptionMustSelectProduct.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillSubscriptionMustEnterAccountNumber {
			get {
				return _rm.GetString( Keys.BillSubscriptionMustEnterAccountNumber.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillSubscriptionInvalidAccountNumber {
			get {
				return _rm.GetString( Keys.BillSubscriptionInvalidAccountNumber.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string ConfirmationPrompt {
			get {
				return _rm.GetString( Keys.ConfirmationPrompt.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Bill {
			get {
				return _rm.GetString( Keys.Bill.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillProduct {
			get {
				return _rm.GetString( Keys.BillProduct.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillPayment {
			get {
				return _rm.GetString( Keys.BillPayment.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillSubscription {
			get {
				return _rm.GetString( Keys.BillSubscription.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillUnsubscription {
			get {
				return _rm.GetString( Keys.BillUnsubscription.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillsPending {
			get {
				return _rm.GetString( Keys.BillsPending.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillProductExternalAccountTooltip {
			get {
				return _rm.GetString( Keys.BillProductExternalAccountTooltip.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillProductExternalAccountIdentifier {
			get {
				return _rm.GetString( Keys.BillProductExternalAccountIdentifier.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillProductExternalAccount {
			get {
				return _rm.GetString( Keys.BillProductExternalAccount.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Subscribed {
			get {
				return _rm.GetString( Keys.Subscribed.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Help {
			get {
				return _rm.GetString( Keys.Help.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Home {
			get {
				return _rm.GetString( Keys.Home.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string NewUser {
			get {
				return _rm.GetString( Keys.NewUser.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string EnrollNow {
			get {
				return _rm.GetString( Keys.EnrollNow.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string ForgotYourID {
			get {
				return _rm.GetString( Keys.ForgotYourID.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string ForgotYourPwd {
			get {
				return _rm.GetString( Keys.ForgotYourPwd.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string ErrorUnknown {
			get {
				return _rm.GetString( Keys.ErrorUnknown.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string ErrorGeneralMsg {
			get {
				return _rm.GetString( Keys.ErrorGeneralMsg.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string LowerLimit {
			get {
				return _rm.GetString( Keys.LowerLimit.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Today {
			get {
				return _rm.GetString( Keys.Today.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AmountShoulBePositive {
			get {
				return _rm.GetString( Keys.AmountShoulBePositive.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AmountRequired {
			get {
				return _rm.GetString( Keys.AmountRequired.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Resume {
			get {
				return _rm.GetString( Keys.Resume.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string Back {
			get {
				return _rm.GetString( Keys.Back.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string FxRate {
			get {
				return _rm.GetString( Keys.FxRate.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BalanceUsd {
			get {
				return _rm.GetString( Keys.BalanceUsd.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string InvalidSubscription {
			get {
				return _rm.GetString( Keys.InvalidSubscription.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillPaymentMustSelectBill {
			get {
				return _rm.GetString( Keys.BillPaymentMustSelectBill.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string PleaseSelect {
			get {
				return _rm.GetString( Keys.PleaseSelect.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AccountFormat {
			get {
				return _rm.GetString( Keys.AccountFormat.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AmountAndFxRateFormat {
			get {
				return _rm.GetString( Keys.AmountAndFxRateFormat.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillAmountExceedsFunds {
			get {
				return _rm.GetString( Keys.BillAmountExceedsFunds.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string BillAmountExceedsLimit {
			get {
				return _rm.GetString( Keys.BillAmountExceedsLimit.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AccountNotFound {
			get {
				return _rm.GetString( Keys.AccountNotFound.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string AccountFormatOther {
			get {
				return _rm.GetString( Keys.AccountFormatOther.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string ErrorMessagePattern {
			get {
				return _rm.GetString( Keys.ErrorMessagePattern.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string InsufficientFunds {
			get {
				return _rm.GetString( Keys.InsufficientFunds.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string LoginGoButtonImage {
			get {
				return _rm.GetString( Keys.LoginGoButtonImage.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string LoginHeaderImage {
			get {
				return _rm.GetString( Keys.LoginHeaderImage.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string LoginFooterImage {
			get {
				return _rm.GetString( Keys.LoginFooterImage.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string CalendarIcon {
			get {
				return _rm.GetString( Keys.CalendarIcon.ToString(CultureInfo.InvariantCulture) );
			}
		}
		static public string GlobalBankBanner {
			get {
				return _rm.GetString( Keys.GlobalBankBanner.ToString(CultureInfo.InvariantCulture) );
			}
		}


	}
}
