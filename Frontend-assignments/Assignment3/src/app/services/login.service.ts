import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of, switchMap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Collector } from '../models/collector.model';

const { apiPokemonCollectors, apiKey } = environment;

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  //injector
  constructor(private readonly http: HttpClient) {}

  /**
   *
   * A function that checks if the user exists
   *
   * @param username the username string
   * @returns the collector or undefined
   */
  private checkUsername(username: string): Observable<Collector | undefined> {
    return this.http
      .get<Collector[]>(`${apiPokemonCollectors}?username=${username}`)
      .pipe(map((response: Collector[]) => response.pop()));
  }

  /**
   *
   * A function that creates a collector user
   *
   * @param username string value of username
   * @returns a Collector object
   */
  public createCollector(username: string): Observable<Collector> {
    return this.checkUsername(username).pipe(
      switchMap((collector: Collector | undefined) => {
        if (collector === undefined) {
          const collector = {
            username,
            pokemons: [],
          };
          const headers = new HttpHeaders({
            'Content-type': 'application/json',
            'x-api-key': apiKey,
          });

          return this.http.post<Collector>(apiPokemonCollectors, collector, {
            headers,
          });
        } else {
          collector = undefined;
        }

        return of(collector!);
      })
    );
  }
  /**
   *
   * A function that returns the collector from API
   *
   * @param username the username string
   * @returns the collector
   */
  public loginUser(username: string) {
    return this.checkUsername(username).pipe(
      switchMap((collector: Collector | undefined) => {
        if (collector === undefined) {
          collector = undefined;
          return of(collector!);
        }

        return of(collector);
      })
    );
  }
}
