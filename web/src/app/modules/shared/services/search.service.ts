import { EventEmitter, Injectable } from "@angular/core";

import { SearchFormDeliveryIncomes } from "../models/search";

@Injectable({
  providedIn: "root",
})
export class SearchServiceDeliveryIncomes {
  searchFormEvent = new EventEmitter<SearchFormDeliveryIncomes>();

  private searchForm: SearchFormDeliveryIncomes;

  public get getSearchForm(): SearchFormDeliveryIncomes {
    if (!this.searchForm) {
      const jsonSearch = localStorage.getItem("SearchFormDeliveryIncomes");
      this.searchForm = jsonSearch ? JSON.parse(jsonSearch) : null;
    }
    return this.searchForm;
  }

  storeSearch(searchFormParams: SearchFormDeliveryIncomes) {
    this.searchForm = searchFormParams;
    this.searchFormEvent.next(searchFormParams);
    localStorage.setItem(
      "SearchFormDeliveryIncomes",
      JSON.stringify(searchFormParams)
    );
  }

  removeSearch() {
    this.searchForm = {
      endDate: null,
      establishmentName: null,
      searchActived: false,
      paidByEstablishment: null,
      paidToLessee: null,
      startDate: null,
    };

    this.searchFormEvent.next(this.searchForm);
    localStorage.setItem(
      "SearchFormDeliveryIncomes",
      JSON.stringify(this.searchForm)
    );
  }
}
