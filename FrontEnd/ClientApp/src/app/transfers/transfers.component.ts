import { Component } from '@angular/core';
import { TransferService } from '../../services/transferService';
import { Transfer } from '../../models/transfer';
import { ResultData } from '../../models/resultData';
import { Router } from '@angular/router';
import Utility from 'src/utility/utility';

@Component({
  selector: 'app-transfers',
  templateUrl: './transfers.component.html'
})
export class TransfersComponent {

  transfers: Transfer[] = [];

  constructor(private transferService: TransferService, private router: Router) {

  }

  ngOnInit(): void {
    this.getAll();
  }

  getAll(): void {
    this.transferService.getAll().subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.transfers = resultData.data as Transfer[];
      } else {
        // Errore
      }
    });
  }

  detailPlayer(id: number): void {
    Utility.redirect('/player/detail/' + id, this.router);    
  }
}
